using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public BoardController BoardController;
    public SolveAlgorithmFactory SolveAlgorithmFactory;

    public void RebuildBoard(string size)
    {
        BoardController.CreateBoard(int.Parse(size));
    }
    public void Solve(int choosenAlgorithm)
    {
        Debug.Log($"solver {choosenAlgorithm}");
        IPathFindingAlgorithm solver = SolveAlgorithmFactory.GetPathFindingAlgorithm((PathFindingAlgorithms)choosenAlgorithm);
        BoardController.SolveBoard(solver);
    }
}
