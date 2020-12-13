using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class IPathFindingAlgorithm
{
    public abstract List<GridElement> SolvePath(GridElement startPoint, GridElement endPoint, List<GridElement> freePositions);

    public virtual List<GridElement> GetAdjacentTiles(GridElement current, List<GridElement> freePositions)
    {
        Vector2 currentPosition = new Vector2(current.Position.x, current.Position.y);
        List<Vector2> adjacentTilesPositions = new List<Vector2>()
            {
                currentPosition + new Vector2( 1,  0),
                currentPosition + new Vector2(-1,  0),
                currentPosition + new Vector2( 0,  1),
                currentPosition + new Vector2( 0, -1),
            };

        var adjacentTiles = freePositions.Where(x => adjacentTilesPositions.Any(y => new Vector2(x.Position.x, x.Position.y).Equals(y))).ToList();

        return adjacentTiles;
    }
    public virtual float GetDistance(Vector2 pos1, Vector2 pos2)
    {
        return Vector2.Distance(pos1, pos2);
    }
}
