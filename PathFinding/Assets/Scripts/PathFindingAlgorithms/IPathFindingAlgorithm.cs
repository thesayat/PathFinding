using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IPathFindingAlgorithm
{
    public abstract List<Vector2> SolvePath(Vector2 startPoint, Vector2 endPoint, List<Vector2> freePositions);
}
