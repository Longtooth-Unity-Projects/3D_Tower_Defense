using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ORDER: dependent on GridManager executing first


public class Pathfinder : MonoBehaviour
{
    [SerializeField] Vector2Int initialStartCoordinates;
    public Vector2Int StartCoordinates { get { return initialStartCoordinates; } }

    [SerializeField] Vector2Int destinationCoordinates;
    public Vector2Int DestinationCoordinates { get { return destinationCoordinates; } }

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
        {
            grid = gridManager.Grid;
            startNode = grid[initialStartCoordinates];
            destinationNode = grid[destinationCoordinates];
        }
    }


    void Start()
    {
        GetNewPath();
    }


    public List<Node> GetNewPath()
    {
        return GetNewPath(initialStartCoordinates);
    }

    public List<Node> GetNewPath(Vector2Int startingCoordinates)
    {
        gridManager.ResetNodes();
        BreadthFirstSearch(startingCoordinates);
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


    //TODO change this to be able to pass in destination node as well
    private void BreadthFirstSearch(Vector2Int startingCoordinates)
    {
        //ensures nodes are walkable for pathfinding but remain unplaceable for towers
        startNode.isWalkable = true;
        destinationNode.isWalkable = true;

        frontierNodes.Clear();
        reachedNodes.Clear();

        bool isRunning = true;

        frontierNodes.Enqueue(grid[startingCoordinates]);
        reachedNodes.Add(startingCoordinates, grid[startingCoordinates]);

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

    //TODO change this to traditional events because we don't like strings
    public void BroadcastRecalculatePath()
    {
        BroadcastMessage("RecalculatePath", false, SendMessageOptions.DontRequireReceiver);
    }


}// end of class
