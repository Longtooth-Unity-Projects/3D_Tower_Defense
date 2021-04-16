using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Defender defenderPrefab;

    [SerializeField] private bool isPlaceable = true;
    public bool IsPlaceable    { get { return isPlaceable; } }

    //cached references
    private GridManager gridManager;
    private Pathfinder pathfinder;
    Vector2Int coordinates = new Vector2Int();

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }


    private void Start()
    {
        if (gridManager != null)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);

            //TODO: possibly move this functionality to the grid manager
            //TODO: bug this might need to be placed elswhere as the path is still going over these tiles that are not placeable
            //make tile that are not placeable correspond to blocked nodes in grid
            if (!isPlaceable)
                gridManager.BlockNodeWalkable(coordinates);
        }
    }



    public void OnPointerDown(PointerEventData eventData)
    {
        //TODO rework this logic, clicking with no resources still blocks one square while it is selected
        //this probably happens because willblockpath is called on the click instead of after place
        if(gridManager.GetNode(coordinates).isWalkable && !pathfinder.WillBlockPath(coordinates))
        {
            // we want to make a tile nonplaceable if when a defender is placed on it
            bool placementSuccessful = defenderPrefab.SpawnDefender(defenderPrefab, transform.position);

            //if we were unable to place a defender, then we should not block the tile
            if(placementSuccessful)
            {
                gridManager.BlockNodeWalkable(coordinates);
                pathfinder.BroadcastRecalculatePath();
            }
        }
    }
}
