using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways]
public class CoordinateLabeler : MonoBehaviour
{
    private TextMeshPro coordinateLabel;
    private Vector2Int coordinates = new Vector2Int();
    private float tileSizeInWorldUnits;

    private void Awake()
    {
        coordinateLabel = GetComponent<TextMeshPro>();
        tileSizeInWorldUnits = UnityEditor.EditorSnapSettings.move.x;

        //so coordinates are shown in play mode
        DisplayCoordinates();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Application.isPlaying)
        {
            //so coordinates are updated in edit mode
            DisplayCoordinates();
            UpdateTileName();
        }
    }

    private void DisplayCoordinates()
    {
        //using the world z coordinate as the game y coordiate due to having a topdown view
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / tileSizeInWorldUnits);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / tileSizeInWorldUnits);
        coordinateLabel.text = $"{coordinates.x},{coordinates.y}";
    }

    private void UpdateTileName()
    {
        transform.parent.name = coordinates.ToString();
    }
}
