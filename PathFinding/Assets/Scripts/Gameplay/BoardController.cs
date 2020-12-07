using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BoardController : MonoBehaviour
{

    public TileObjectPool TileObjectPool;

    [HideInInspector]
    public List<Vector2> AvailableGridPositions = new List<Vector2>();

    private List<GameObject> _tilesInUse = new List<GameObject>();

    public void CreateBoard(int size = 10){
        foreach(var tile in _tilesInUse)
        {
            tile.SetActive(false);
        }
        _tilesInUse.Clear();
        AvailableGridPositions.Clear();

        CreateTilesPositions(size);
    }
    
    private void CreateTilesPositions(int size)
    {
        for (int i = 0; i < size; ++i)
        {
            for (int j = 0; j < size; ++j)
            {
                var newPos = new Vector2(i, j);
                AvailableGridPositions.Add(newPos);

                var newTile = TileObjectPool.GetTile();
                newTile.transform.position = new Vector3(newPos.x, newPos.y, 0);

                _tilesInUse.Add(newTile);
            }
        }
    }
}
