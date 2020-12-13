using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IPathFindingAlgorithm
{
    public abstract List<GridElement> SolvePath(GridElement startPoint, GridElement endPoint, List<GridElement> freePositions);
}
