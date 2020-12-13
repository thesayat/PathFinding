using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public BoardController BoardController;
    public SolveAlgorithmFactory SolveAlgorithmFactory;

    public void SetBoardSize(string size)
    {
        BoardController.SetBoardSize(int.Parse(size));
    }

    public void RebuildBoard()
    {
        BoardController.CreateBoard();
    }

    public void SetBoardSolver(int choosenAlgorithm)
    {
        IPathFindingAlgorithm solver = SolveAlgorithmFactory.GetPathFindingAlgorithm((PathFindingAlgorithms)choosenAlgorithm);
        if(solver != null)
        {
            BoardController.SetBoardSolver(solver);
        }
    }
    public void Solve()
    {
        BoardController.SolveBoard();
    }
}
