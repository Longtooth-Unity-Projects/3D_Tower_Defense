using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.InputSystem;

[ExecuteAlways]
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] private Color defaultColor = Color.black;
    [SerializeField] private Color blockedColor = Color.red;

    private TextMeshPro coordinateLabel;
    private Vector2Int coordinates = new Vector2Int();
    private Waypoint waypoint;

    private float tileSizeInWorldUnits;

    private void Awake()
    {
        coordinateLabel = GetComponent<TextMeshPro>();
        coordinateLabel.enabled = false;

        waypoint = GetComponentInParent<Waypoint>();
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
            coordinateLabel.enabled = true;
        }

        SetCoordinateLabelColor();
        ToggleLabels();
    }

    private void SetCoordinateLabelColor()
    {
        if (waypoint.IsPlaceable)
            coordinateLabel.color = defaultColor;
        else
            coordinateLabel.color = blockedColor;
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

    private void ToggleLabels()
    {
        //TODO change this to an input action map
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            coordinateLabel.enabled = !coordinateLabel.IsActive();
        }
    }
}
