using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BoardController : MonoBehaviour
{
    public TileObjectPool TileObjectPool;

    [HideInInspector]
    public List<GridElement> AvailableGridPositions = new List<GridElement>();

    private List<GameObject> _tilesInUse = new List<GameObject>();

    private int _numberOfObstacles;
    private int _boardSize;
    private GridElement _startPoint;
    private GridElement _endPoint;
    private IPathFindingAlgorithm _solver;

    public void CreateBoard(){
        foreach(var tile in _tilesInUse)
        {
            tile.SetActive(false);
            tile.GetComponent<Tile>().SetTileType(TileTypes.Floor);
        }
        _tilesInUse.Clear();
        AvailableGridPositions.Clear();

        CreateTiles(_boardSize);

        _numberOfObstacles = (_boardSize * _boardSize) / 5;
        CreateObstacles(_numberOfObstacles);

        CreateStartAndEndPoint();
    }
    
    private void CreateTiles(int size)
    {
        int elementID = 0;
        for (int i = 0; i < size; ++i)
        {
            for (int j = 0; j < size; ++j)
            {
                var newPos = new Vector2(i, j);
                AvailableGridPositions.Add(new GridElement(elementID++, newPos));

                var newTile = TileObjectPool.GetTile();
                newTile.transform.position = new Vector3(newPos.x, newPos.y, 0);

                _tilesInUse.Add(newTile);
            }
        }
    }
    private void CreateObstacles(int numberOfObstacles)
    {
        for(int i = 0; i < numberOfObstacles; ++i)
        {
            var obstaclePos = AvailableGridPositions[Random.Range(0, AvailableGridPositions.Count - 1)];
            TileTypes tileType = AdjustTileTypeDependOnBorderConditions(obstaclePos.Position, (TileTypes)Random.Range((int)TileTypes.Obstacle1x1, (int)TileTypes.Obstacle2x2));

            CreateSingleObstacle(obstaclePos);

            if (tileType == TileTypes.Obstacle1x2)
            {
                CreateSingleObstacle(obstaclePos + new Vector2(0f, 1f));
            }
            else if (tileType == TileTypes.Obstacle2x1)
            {
                CreateSingleObstacle(obstaclePos + new Vector2(1f, 0f));
            }
            else if (tileType == TileTypes.Obstacle2x2)
            {
                CreateSingleObstacle(obstaclePos + new Vector2(0f, 1f));
                CreateSingleObstacle(obstaclePos + new Vector2(1f, 0f));
                CreateSingleObstacle(obstaclePos + new Vector2(1f, 1f));
            }
        }
    }
    private TileTypes AdjustTileTypeDependOnBorderConditions(Vector2 obstaclePos, TileTypes tileType)
    {
        if (((int)obstaclePos.x == (_boardSize - 1) && (tileType == TileTypes.Obstacle2x1 || tileType == TileTypes.Obstacle2x2)) ||
            ((int)obstaclePos.y == (_boardSize - 1) && (tileType == TileTypes.Obstacle1x2 || tileType == TileTypes.Obstacle2x2)))
        {
            return TileTypes.Obstacle1x1;
        }
        return tileType;
    }
    private void CreateSingleObstacle(GridElement obstaclePos)
    {
        var possiblePos = AvailableGridPositions.Where(x => x.Position.Equals(obstaclePos.Position)).ToList();

        if(possiblePos.Count == 0)
        {
            return;
        }

        AvailableGridPositions.Remove(possiblePos[0]);
        var newTile = TileObjectPool.GetTile();
        newTile.transform.position = new Vector3(obstaclePos.Position.x, obstaclePos.Position.y, 0);
        newTile.GetComponent<Tile>().SetTileType(TileTypes.Obstacle1x1);
        _tilesInUse.Add(newTile);
    }
    private void CreateStartAndEndPoint()
    {
        _startPoint = AvailableGridPositions[Random.Range(0, AvailableGridPositions.Count - 1)];
        AvailableGridPositions.Remove(_startPoint);
        var newTile = TileObjectPool.GetTile();
        newTile.transform.position = new Vector3(_startPoint.Position.x, _startPoint.Position.y, 0);
        newTile.GetComponent<Tile>().SetTileType(TileTypes.StartPoint);
        _tilesInUse.Add(newTile);

        _endPoint = AvailableGridPositions[Random.Range(0, AvailableGridPositions.Count - 1)];
        AvailableGridPositions.Remove(_endPoint);
        var newTileEnd = TileObjectPool.GetTile();
        newTileEnd.transform.position = new Vector3(_endPoint.Position.x, _endPoint.Position.y, 0);
        newTileEnd.GetComponent<Tile>().SetTileType(TileTypes.EndPoint);
        _tilesInUse.Add(newTileEnd);
    }
    public bool SolveBoard()
    {
        var availablePositionsWithStartEndPoints = new List<GridElement>(AvailableGridPositions);
        availablePositionsWithStartEndPoints.Add(_startPoint);
        availablePositionsWithStartEndPoints.Add(_endPoint);

        var path = _solver.SolvePath(_startPoint, _endPoint, availablePositionsWithStartEndPoints);

        if(pathIsEmpty(path))
        {
            return false;
        }

        path.ForEach(x =>
        {
            var newTile = TileObjectPool.GetTile();
            newTile.transform.position = new Vector3(x.Position.x, x.Position.y, 0);
            newTile.GetComponent<Tile>().SetTileType(TileTypes.Path);
            _tilesInUse.Add(newTile);
        });

        return true;
    }
    private bool pathIsEmpty(List<GridElement> path)
    {
        return (path != null && path.Count == 0);
    }
    public void SetBoardSize(int boardSize)
    {
        _boardSize = boardSize;
    }
    public void SetBoardSolver(IPathFindingAlgorithm solver)
    {
        _solver = solver;
    }
}
