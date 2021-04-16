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
    [SerializeField] private Color exploredColor = Color.yellow;
    [SerializeField] private Color pathColor = new Color(1f, 0.5f, 0f); //orange

    private TextMeshPro coordinateLabel;
    private Vector2Int coordinates = new Vector2Int();
    private GridManager gridManager;

    private bool gameRunning = true;




    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        coordinateLabel = GetComponent<TextMeshPro>();
        coordinateLabel.enabled = false;

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
        TogglePause();
    }

    private void DisplayCoordinates()
    {
        if (gridManager == null) { return; }

        //using the world z coordinate as the game y coordiate due to having a topdown view
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.UnityGridSquareSize);
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.UnityGridSquareSize);
        coordinateLabel.text = $"{coordinates.x},{coordinates.y}";
    }

    private void UpdateTileName()
    {
        transform.parent.name = coordinates.ToString();
    }


    private void SetCoordinateLabelColor()
    {
        if (gridManager == null) { return; }
        Node node = gridManager.GetNode(coordinates);
        if(node == null) { return; }


        if (!node.isWalkable)
            coordinateLabel.color = blockedColor;
        else if (node.isInPath)
            coordinateLabel.color = pathColor;
        else if (node.isExplored)
            coordinateLabel.color = exploredColor;
        else
            coordinateLabel.color = defaultColor;
    }


    private void ToggleLabels()
    {
        //TODO change this to an input action map
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            coordinateLabel.enabled = !coordinateLabel.IsActive();
        }
    }

    private void TogglePause()
    {
        //TODO change this to an input action map
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            gameRunning = !gameRunning;
        }

        if(gameRunning)
            Time.timeScale = 1;
        else
            Time.timeScale = 0;
    }
}
