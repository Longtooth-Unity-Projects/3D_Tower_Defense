using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Vector2Int startCoordinates;
    [SerializeField] Vector2Int destinationCoordinates;

    private Node startNode;
    private Node destinationNode;
    private Node currentSearchNode;

    //list of nodes that have not been explored
    Queue<Node> frontierNodes = new Queue<Node>();
    //list of nodes that have been added to tree
    Dictionary<Vector2Int, Node> reachedNodes = new Dictionary<Vector2Int, Node>(); 

    Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };
    
    private GridManager gridManager;
    private Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();

        if (gridManager != null)
            grid = gridManager.Grid;
    }


    void Start()
    {
        startNode = grid[startCoordinates];
        destinationNode = grid[destinationCoordinates];

        GetNewPath();
    }


    public List<Node> GetNewPath()
    {
        gridManager.ResetNodes();
        BreadthFirstSearch();
        return BuildPath();
    }

    private void ExploreNeighbors()
    {
        List<Node> neighbors = new List<Node>();

        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighborCoordinates = currentSearchNode.coordinates + direction;

            if(grid.ContainsKey(neighborCoordinates))
            {
                neighbors.Add(grid[neighborCoordinates]);
            }
        }

        foreach (Node neighbor in neighbors)
        {
            if(!reachedNodes.ContainsKey(neighbor.coordinates) && neighbor.isWalkable)
            {
                neighbor.connectedTo = currentSearchNode;
                reachedNodes.Add(neighbor.coordinates, neighbor);
                frontierNodes.Enqueue(neighbor);
            }
        }
    }// end of ExploreNeighbors()


    private void BreadthFirstSearch()
    {
        frontierNodes.Clear();
        reachedNodes.Clear();

        bool isRunning = true;

        frontierNodes.Enqueue(startNode);
        reachedNodes.Add(startNode.coordinates, startNode);

        while(frontierNodes.Count > 0 && isRunning)
        {
            currentSearchNode = frontierNodes.Dequeue();
            currentSearchNode.isExplored = true;
            ExploreNeighbors();

            if (currentSearchNode.coordinates == destinationCoordinates)
                isRunning = false;
        }
    }//end of BreadthFirstSearch()


    private List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node currentNode = destinationNode;

        path.Add(currentNode);
        currentNode.isInPath = true;

        while(currentNode.connectedTo != null)
        {
            currentNode = currentNode.connectedTo;
            path.Add(currentNode);
            currentNode.isInPath = true;
        }

        path.Reverse();
        return path;
    }//end of BuildPath()


    public bool WillBlockPath(Vector2Int coordinates)
    {
        if(grid.ContainsKey(coordinates))
        {
            bool previousWalkable = grid[coordinates].isWalkable;

            grid[coordinates].isWalkable = false;
            List<Node> newPath = GetNewPath();
            grid[coordinates].isWalkable = previousWalkable;

            //if path is blocked
            if(newPath.Count <= 1)
            {
                GetNewPath();
                return true;
            }
        }
        return false;
    }// end of WillBlockPath()



}// end of class
