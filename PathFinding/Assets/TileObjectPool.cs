using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TileObjectPool : MonoBehaviour
{
    public GameObject TilePrefab;

    public List<GameObject> Tiles;

    private int _pooledTiles = 500;

    private void Awake()
    {
        for (int i = 0; i < _pooledTiles; ++i)
        {
            AddTileToPool();
        }
    }
    public GameObject GetTile()
    {
        var nextFreeTile = Tiles.First(x => !x.activeInHierarchy);

        if(nextFreeTile == null)
        {
            nextFreeTile = AddTileToPool();
        }

        nextFreeTile.SetActive(true);
        return nextFreeTile;
    }
    private GameObject AddTileToPool()
    {
        var newTile = Instantiate(TilePrefab, transform);
        newTile.SetActive(false);
        Tiles.Add(newTile);
        return newTile;
    }
}
