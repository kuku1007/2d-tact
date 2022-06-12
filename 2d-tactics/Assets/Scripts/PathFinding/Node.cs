using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public int hCost;
    public int gCost;
    public int fCost;
    public Vector2Int tilePos;
    public Node parentNode;

    public Node(Vector2Int tilePos) {
        this.tilePos = tilePos;
    }

    public override bool Equals(object obj)
    {
        Node other = (Node) obj;
        return other.tilePos.Equals(this.tilePos);
    }

    public override int GetHashCode()
    {
        return tilePos.GetHashCode();
    }
}
