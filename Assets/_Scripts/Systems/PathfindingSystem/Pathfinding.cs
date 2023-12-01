using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Algorithm for finding a path between two points on a two-dimensional <see cref="Grid"/>.
/// </summary>
[RequireComponent(typeof(Grid))]
[RequireComponent(typeof(PathRequestManager))]
public class Pathfinding : MonoBehaviour
{
    /// <summary>
    /// Additional cost to the diagonal step.
    /// </summary>
    [SerializeField] private int _diagonalStepCoast = 4;

    /// <summary>
    /// The cost of a step horizontally and vertically.
    /// </summary>
    [SerializeField] private int _defaultStepCoast = 10;

    /// <summary>
    /// An instance of the <see cref="PathRequestManager"/> class.
    /// </summary>
    private PathRequestManager _pathRequestManager;

    /// <summary>
    /// An array containing calculated paths between pairs of points on the <see cref="Grid"/>.
    /// </summary>
    private Dictionary<int, Vector3[]> _pathList = new Dictionary<int, Vector3[]>();

    /// <inheritdoc cref="Grid"/>
    private Grid _grid;

    private void Awake()
    {
        _pathRequestManager = GetComponent<PathRequestManager>();
        _grid = GetComponent<Grid>();
    }

    /// <summary>
    /// The method runs the pathfinding algorithm for one of the two existing <see cref="Grid"/>s.
    /// </summary>
    /// <param name="startPosition"> Position of the initial <see cref="Cell"/> in the game world.</param>
    /// <param name="targetPosition"> Position of the target <see cref="Cell"/> in the game world.</param>
    public void StartFindPath(Vector3 startPosition, Vector3 targetPosition)
    {
        float distanceX = Math.Abs(startPosition.x - targetPosition.x);
        float distanceY = Math.Abs(startPosition.y - targetPosition.y);

        if (distanceX <= 12 && distanceY <= 12)
        {
            StartCoroutine(FindPath(startPosition, targetPosition, _grid.maxSize, true));
        }
        else
        {
            StartCoroutine(FindPath(startPosition, targetPosition, _grid.secondGridMaxSize, false));
        }
    }

    /// <summary>
    /// Clears the existing list of paths.
    /// </summary>
    public void ResetPathList() => _pathList.Clear();

    /// <summary>
    /// The method runs the pathfinding algorithm for one of the two existing <see cref="Grid"/>s.
    /// </summary>
    /// <param name="startPosition"> Position of the initial <see cref="Cell"/> in the game world.</param>
    /// <param name="targetPosition"> Position of the target <see cref="Cell"/> in the game world.</param>
    /// <param name="gridMaxSize"> Number of cells in the <see cref="Grid"/>.</param>
    /// <param name="useDefaultGrid"> The boolean variable specifying from which grid the cells will be selected.</param>
    private IEnumerator FindPath(Vector3 startPosition, Vector3 targetPosition, int gridMaxSize, bool useDefaultGrid = true)
    {
        Vector3[] waypoints = new Vector3[0];
        bool pathSuccess = false;

        Cell startNode = _grid.GetCellFromeWorldPoint(startPosition, useDefaultGrid);
        Cell targetNode = _grid.GetCellFromeWorldPoint(targetPosition, useDefaultGrid);

        int pathKey = startNode.GridX * 1000000 + startNode.GridY * 10000 + targetNode.GridX * 100 + targetNode.GridY;

        if (_pathList.ContainsKey(pathKey))
            _pathRequestManager.FinishedProcessingPath(_pathList[pathKey], pathSuccess);
        else
        {
            if (startNode.Walkable && targetNode.Walkable)
            {
                Heap<Cell> openList = new Heap<Cell>(gridMaxSize);
                HashSet<Cell> closeList = new HashSet<Cell>();

                openList.Add(startNode);

                while (openList.Count > 0)
                {
                    Cell currentNode = openList.RemoveFirst();
                    closeList.Add(currentNode);

                    if (currentNode == targetNode)
                    {
                        pathSuccess = true;
                        break;
                    }

                    foreach (Cell neighbour in _grid.GetNeighbours(currentNode, useDefaultGrid))
                    {
                        if (!neighbour.Walkable || closeList.Contains(neighbour)) continue;

                        int newMovementCostToNeighbour = currentNode.GCost + GetMovementCost(currentNode, neighbour) + neighbour.MovmentPenalty;
                        bool neighbourIsExistInOpenList = !openList.Contains(neighbour);

                        if (newMovementCostToNeighbour < neighbour.GCost || neighbourIsExistInOpenList)
                        {
                            neighbour.GCost = newMovementCostToNeighbour;
                            neighbour.HCost = GetMovementCost(neighbour, targetNode);
                            neighbour.Parent = currentNode;

                            if (neighbourIsExistInOpenList)
                                openList.Add(neighbour);
                            else openList.UpdateItem(neighbour);
                        }
                    }
                }
            }
            _pathList.Add(pathKey, waypoints);

            yield return null;

            if (pathSuccess) waypoints = RetracePath(startNode, targetNode);
            _pathRequestManager.FinishedProcessingPath(waypoints, pathSuccess);
        }
    }

    /// <summary>
    /// Returns an array of <see cref="Cell"/>s representing a sequence of steps in the found path.
    /// </summary>
    /// <param name="startNode"> Start <see cref="Cell"/>.</param>
    /// <param name="endNode"> End <see cref="Cell"/>.</param>
    private Vector3[] RetracePath(Cell startNode, Cell endNode)
    {
        List<Cell> path = new List<Cell>();
        Cell currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.Parent;
        }

        return SimplifyPath(path);
    }

    /// <summary>
    /// Simplifies the path by removing <see cref="Cell"/>s that do not affect the change in direction of movement in the found path.
    /// </summary>
    /// <param name="path"> Array of <see cref="Cell"/>s representing a sequence of steps in the found path.</param>
    private Vector3[] SimplifyPath(List<Cell> path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;

        for (int i = 1; i < path.Count; i++)
        {
            float x = path[i - 1].GridX - path[i].GridX;
            float y = path[i - 1].GridY - path[i].GridY;

            Vector2 directionNew = new Vector2(x, y);

            if (directionNew != directionOld)
                waypoints.Add(path[i].WorldPosition);

            directionOld = directionNew;
        }

        Vector3[] pathWaypoints = new Vector3[waypoints.Count];

        for (int i = 0; i < waypoints.Count; i++)
        {
            pathWaypoints[waypoints.Count - i - 1] = waypoints[i];
        }

        return pathWaypoints;
    }

    /// <summary>
    /// Calculates the cost of the path between two <see cref="Cell"/>s.
    /// </summary>
    /// <param name="nodeA"> <see cref="Cell"/>.</param>
    /// <param name="nodeB"> <see cref="Cell"/>.</param>
    private int GetMovementCost(Cell nodeA, Cell nodeB)
    {
        int distX = Mathf.Abs(nodeA.GridX - nodeB.GridX);
        int distY = Mathf.Abs(nodeA.GridY - nodeB.GridY);

        int resalt = distX > distY
            ? _diagonalStepCoast * distY + _defaultStepCoast * distX
            : _diagonalStepCoast * distX + _defaultStepCoast * distY;

        return resalt;
    }
}
