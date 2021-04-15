using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
    public Vector2Int coordinates;
    public bool isWalkable;
    public bool isExplored;
    public bool isInPath;
    public Node connectedTo;

    public Node(Vector2Int coordinates_, bool isWalkable_)
    {
        this.coordinates = coordinates_;
        this.isWalkable = isWalkable_;
    }
}
