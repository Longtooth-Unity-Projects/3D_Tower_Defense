using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
    public Vector2Int m_coordinates;
    public bool m_isWalkable;
    public bool m_isExplored;
    public bool m_isInPath;
    public Node m_connectedTo;

    public Node(Vector2Int coordinates, bool isWalkable)
    {
        m_coordinates = coordinates;
        m_isWalkable = isWalkable;
    }
}
