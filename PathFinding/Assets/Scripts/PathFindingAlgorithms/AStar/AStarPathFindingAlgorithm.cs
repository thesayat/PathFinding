using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AStarPathFindingAlgorithm : IPathFindingAlgorithm
{
    public override List<GridElement> SolvePath(GridElement startPoint, GridElement endPoint, List<GridElement> freePositions)
    {
        var activeTiles = new List<AStarGridElement>();
        var visitedTiles = new List<AStarGridElement>();

        var startTile = new AStarGridElement(startPoint);
        var endTile = new AStarGridElement(endPoint);

        var aStarFreePositions = new List<AStarGridElement>();
        freePositions.ForEach(x => aStarFreePositions.Add(new AStarGridElement(x)));

        activeTiles.Add(startTile);

        while (activeTiles.Any())
        {
            float lowestFCost = activeTiles.Min(x => x.FCost);
            var currentTile = activeTiles.Where(x => x.FCost == lowestFCost).OrderBy(y => y.HCost).First();

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

            var tileNeighboursPos = GetAdjacentTiles(currentTile as GridElement, freePositions);
            var tileNeighbours = new List<AStarGridElement>();
            tileNeighboursPos.ForEach(x =>
            {
                if(x.ID != startPoint.ID)
                {
                    tileNeighbours.Add(new AStarGridElement(x));
                }
            });

            foreach (var neighbour in tileNeighbours)
            {
                if (visitedTiles.Any(x => x.ID == neighbour.ID))
                {
                    continue;
                }
                float newCostToNeighbour = currentTile.GCost + GetDistance(currentTile.Position, neighbour.Position);
                if (newCostToNeighbour < neighbour.GCost || !activeTiles.Any(x => x.ID == neighbour.ID))
                {
                    neighbour.GCost = newCostToNeighbour;
                    neighbour.HCost = GetDistance(neighbour.Position, endTile.Position);
                    neighbour.Parent = currentTile;

                    if (!activeTiles.Any(x => x.ID == neighbour.ID))
                        activeTiles.Add(neighbour);
                }
            }
        }

        return new List<GridElement>();
    }
}