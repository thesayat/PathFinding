using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AStarPathFindingAlgorithm : IPathFindingAlgorithm
{
    class AStarGridElement
    {
        public int ID { get; set; }
        public float X { get; set; }
        public float Y { get; set; }

        public float hCost { get; set; }
        public float gCost { get; set; }

        public float fCost
        {
            get
            {
                return hCost + gCost;
            }
        }

        public AStarGridElement Parent { get; set; }

        public AStarGridElement(GridElement tile)
        {
            ID = tile.ID;
            X = tile.Position.x;
            Y = tile.Position.y;
        }

        public AStarGridElement(GridElement tile, AStarGridElement parent) : this(tile)
        {
            Parent = parent;
        }
    }

    public override List<GridElement> SolvePath(GridElement startPoint, GridElement endPoint, List<GridElement> freePositions)
    {
        var activeTiles = new List<AStarGridElement>();
        var visitedTiles = new List<AStarGridElement>();

        var startTile = new AStarGridElement(startPoint);
        var endTile = new AStarGridElement(endPoint);

        var aStarFreePositions = new List<AStarGridElement>();

        freePositions.Add(startPoint);
        freePositions.Add(endPoint);

        freePositions.ForEach(x => aStarFreePositions.Add(new AStarGridElement(x)));

        activeTiles.Add(startTile);

        while (activeTiles.Any())
        {
            float lowestFCost = activeTiles.Min(x => x.fCost);
            var currentTile = activeTiles.Where(x => x.fCost == lowestFCost).OrderBy(y => y.hCost).First();

            activeTiles.Remove(currentTile);
            visitedTiles.Add(currentTile);

            if(currentTile.ID == endTile.ID)
            {
                var path = new List<GridElement>();

                while (currentTile != null)
                {
                    if (currentTile.ID != startPoint.ID && currentTile.ID != endPoint.ID)
                    {
                        var gridElement = freePositions.First(x => x.ID == currentTile.ID);
                        path.Add(gridElement);
                    }
                    currentTile = currentTile.Parent;
                }

                return path;
            }

            var tileNeighbours = getAdjacentTiles(currentTile, aStarFreePositions);

            foreach(var neighbour in tileNeighbours)
            {
                if (visitedTiles.Contains(neighbour))
                {
                    continue;
                }
                float newCostToNeighbour = currentTile.gCost + GetDistance(currentTile, neighbour);
                if (newCostToNeighbour < neighbour.gCost || !activeTiles.Contains(neighbour))
                {
                    neighbour.gCost = newCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, endTile);
                    neighbour.Parent = currentTile;

                    if (!activeTiles.Contains(neighbour))
                        activeTiles.Add(neighbour);
                }
            }
        }

        return new List<GridElement>();
    }
    float GetDistance(AStarGridElement nodeA, AStarGridElement nodeB)
    {
        float dstX = Mathf.Abs(nodeA.X - nodeB.X);
        float dstY = Mathf.Abs(nodeA.Y - nodeB.Y);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }
    List<AStarGridElement> getAdjacentTiles(AStarGridElement current, List<AStarGridElement> freePositions)
    {
        Vector2 currentPosition = new Vector2(current.X, current.Y);
        List<Vector2> adjacentTilesPositions = new List<Vector2>()
            {
                currentPosition + new Vector2( 1,  0),
                currentPosition + new Vector2(-1,  0),
                currentPosition + new Vector2( 0,  1),
                currentPosition + new Vector2( 0, -1),
            };

        var adjacentTiles = freePositions.Where(x => adjacentTilesPositions.Any(y => new Vector2(x.X, x.Y).Equals(y))).ToList();

        return adjacentTiles;
    }
    bool ValuesAreEqual(float val1, float val2)
    {
        double epsilon = 0.0001;
        return (Math.Abs(val1 - val2) < epsilon);
    }
}