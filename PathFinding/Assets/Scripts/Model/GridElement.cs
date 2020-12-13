using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridElement
{
    public int ID;
    public Vector2 Position;

    public GridElement(int id, Vector2 pos)
    {
        ID = id;
        Position = pos;
    }

    public static GridElement operator+(GridElement gridElement, Vector2 vec)
    {
        gridElement.Position += vec;
        return gridElement;
    }

}
