using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public TileTypes TileType;

    public void SetTileType(TileTypes tileType)
    {
        TileType = tileType;

		try
		{
			GetComponent<SpriteRenderer>().color = GetTileColor();
		}
		catch (System.Exception e)
		{
			Debug.Log("Couldn't retrieve SpriteRenderer component at Tile.cs");
			throw e;
		}
    }
	Color GetTileColor() 
	{
		switch (TileType)
		{
			case TileTypes.Path:
				return Color.green;
			case TileTypes.StartPoint:
				return Color.yellow;
			case TileTypes.EndPoint:
				return Color.blue;
			case TileTypes.Obstacle1x1:
			case TileTypes.Obstacle1x2:
			case TileTypes.Obstacle2x1:
				return Color.red;
			case TileTypes.Floor:
			default:
				return Color.white;
		}
	}
}
