using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    //TODO change this to be dynamic based on the playing field size
    [SerializeField] private Vector2Int gridSize = new Vector2Int(21,11);

    [Tooltip("World Grid Square Size should match Unity editor snap settings")]
    [SerializeField] private int unityGridSquareSize = 10;
    public int UnityGridSquareSize { get { return unityGridSquareSize; } }

    private Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    public Dictionary<Vector2Int, Node> Grid { get { return grid; } }



    private void Awake()
    {
        CreateGrid();
    }



    public void BlockNode(Vector2Int coordinates)
    {
        if (grid.ContainsKey(coordinates))
            grid[coordinates].isWalkable = false;
    }


    public void ResetNodes()
    {
        foreach (KeyValuePair<Vector2Int, Node> entry in grid)
        {
            entry.Value.connectedTo = null;
            entry.Value.isExplored = false;
            entry.Value.isInPath = false;
        }
    }


    public Vector2Int GetCoordinatesFromPosition(Vector3 position)
    {
        Vector2Int coordinates = new Vector2Int();

        //using the world z coordinate as the game y coordiate due to having a topdown view
        coordinates.x = Mathf.RoundToInt(position.x / unityGridSquareSize);
        coordinates.y = Mathf.RoundToInt(position.z / unityGridSquareSize);

        return coordinates;
    }


    public Vector3 GetPostionFromCoordinates(Vector2Int coordinates)
    {
        Vector3 position = new Vector3(0,0,0);

        position.x = coordinates.x * unityGridSquareSize;
        position.z = coordinates.y * unityGridSquareSize;

        return position;
    }

    //custom methods and functions
    private void CreateGrid()
    {
        for (int x = 0; x < gridSize.x; ++x)
        {
            for (int y = 0; y < gridSize.y; ++y)
            {
                Vector2Int coordinates = new Vector2Int(x, y);
                grid.Add(coordinates, new Node(coordinates, true));
            }
        }
    }





    //accessors
    public Node GetNode(Vector2Int coordinate)
    {
        if(grid.ContainsKey(coordinate))
        {
            return grid[coordinate];
        }

        return null;
    }

}
