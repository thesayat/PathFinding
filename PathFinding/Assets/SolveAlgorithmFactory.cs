using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolveAlgorithmFactory : MonoBehaviour
{
    public IPathFindingAlgorithm GetPathFindingAlgorithm(PathFindingAlgorithms pathFindingAlgorithm)
    {
        switch (pathFindingAlgorithm)
        {
            case PathFindingAlgorithms.AStar:
                return new AStarPathFindingAlgorithm();
            case PathFindingAlgorithms.None:
            default:
                return null;
        }
    }

}
