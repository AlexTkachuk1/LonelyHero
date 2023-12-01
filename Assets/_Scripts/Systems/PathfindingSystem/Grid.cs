using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// The grid of cells created for the pathfinding algorithm <see cref="Pathfinding"/>.
/// </summary>
public class Grid : MonoBehaviour
{
    #region Properties
    /// <summary>
    /// Boolean variable enabling rendering first grid.
    /// </summary>
    public bool DisplayGridGizmous;

    /// <summary>
    /// Boolean variable enabling rendering second grid.
    /// </summary>
    public bool DisplaySecondGridGizmous;

    /// <summary>
    /// Level <see cref="Tilemap"/>.
    /// </summary>
    [SerializeField] private Tilemap _level;
    [SerializeField] private Tilemap _ground;

    /// <summary>
    /// Player <see cref="Transform"/>.
    /// </summary>
    [SerializeField] private Transform _player;

    /// <summary>
    /// Array of <see cref="TerrainType"/>.
    /// </summary>
    [SerializeField] private TerrainType[] _walkableRegions;

    /// <summary>
    /// Grid size in two-dimensional coordinate plane.
    /// </summary>
    private Vector2 _gridWorldSize;

    private float _gridWorldSizeOffSetX = 4f, _gridWorldSizeOffSetY = 2f;

    /// <summary>
    /// Layer containing objects of physical obstacles.
    /// </summary>
    [SerializeField] private LayerMask _obstacles;

    /// <summary>
    /// The dictionary containing as a key the index of the layer in the unit,
    /// and as the corresponding value of the penalty for moving through the entrances
    /// located in this layer in the pathfinding algorithm <see cref="Pathfinding"/>.
    /// </summary>
    private readonly Dictionary<int, int> _walkableRegionsDictionary = new Dictionary<int, int>();

    /// <summary>
    /// Layer on the cells of which you can move.
    /// </summary>
    private LayerMask _walkableMask;

    [SerializeField] private GridInstance _default, _second;
    #endregion

    /// <summary>
    /// Number of <see cref="Cell"/>s in the <see cref="Grid"/>.
    /// </summary>
    public int maxSize
    {
        get
        {
            return _default.GridSizeX * _default.GridSizeY;
        }
    }

    /// <inheritdoc cref="maxSize"/>
    public int secondGridMaxSize
    {
        get
        {
            return _second.GridSizeX * _second.GridSizeY;
        }
    }

    private void Awake()
    {
        _gridWorldSize = new Vector2((Mathf.Abs(_ground.cellBounds.xMin) + Mathf.Abs(_ground.cellBounds.xMax)) - _gridWorldSizeOffSetX,
            (Mathf.Abs(_ground.cellBounds.yMin) + Mathf.Abs(_ground.cellBounds.yMax)) - _gridWorldSizeOffSetY);

        _default.CellDiametor = _default.CellRadius * 2;
        _default.GridSizeX = Mathf.RoundToInt(_gridWorldSize.x / _default.CellDiametor);
        _default.GridSizeY = Mathf.RoundToInt(_gridWorldSize.y / _default.CellDiametor);

        _second.CellDiametor = _second.CellRadius * 2;
        _second.GridSizeX = Mathf.RoundToInt(_gridWorldSize.x / _second.CellDiametor);
        _second.GridSizeY = Mathf.RoundToInt(_gridWorldSize.y / _second.CellDiametor);

        foreach (TerrainType region in _walkableRegions)
        {
            _walkableMask.value += region.terrainMask.value;
            _walkableRegionsDictionary.Add((int)Mathf.Log(region.terrainMask.value, 2), region.terrainPanalty);
        }

        _default.GridArray = CreateGrid(_default.GridSizeX, _default.GridSizeY, _default.CellRadius, _default.CellDiametor);
        _second.GridArray = CreateGrid(_second.GridSizeX, _second.GridSizeY, _second.CellRadius, _second.CellDiametor);
    }

    /// <summary>
    /// Creates a <see cref="Grid"/> of <see cref="Cell"/>s.
    /// </summary>
    /// <param name="gridSizeX"> The number of cells that the grid includes along the x axis.</param>
    /// <param name="gridSizeY"> The number of cells that the grid includes along the y axis.</param>
    /// <param name="CellRadius"> Radius of <see cref="Cell"/>s of the <see cref="Grid"/>.</param>
    /// <param name="CellDiametor"> Diametor of <see cref="Cell"/>s of the <see cref="Grid"/>.</param>
    private Cell[,] CreateGrid(int gridSizeX, int gridSizeY, float CellRadius, float CellDiametor)
    {
        Cell[,] grid = new Cell[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right
            * _gridWorldSize.x / 2 - Vector3.up * _gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft
                    + Vector3.right * (x * CellDiametor + CellRadius)
                    + Vector3.up * (y * CellDiametor + CellRadius);

                var celledPosition = _level.WorldToCell(worldPoint);

                bool walkable = _level.GetTile(celledPosition) == null;

                int movmentPenalty = 0;

                if (walkable)
                {
                    Ray ray = new Ray(worldPoint + Vector3.up * 50, Vector3.down);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, 100, _walkableMask))
                        _walkableRegionsDictionary.TryGetValue(hit.collider.gameObject.layer, out movmentPenalty);
                }

                grid[x, y] = new Cell(walkable, worldPoint, x, y, movmentPenalty);
            }
        }
        return grid;
    }

    /// <summary>
    /// Returns a list of nearby <see cref="Cell"/>s that can be walked on.
    /// </summary>
    /// <param name="cell"> One of the <see cref="Grid"/> <see cref="Cell"/>s.</param>
    /// <param name="useDefaultGrid"> The boolean variable specifying from which grid the nearest cells will be selected.</param>
    internal List<Cell> GetNeighbours(Cell cell, bool useDefaultGrid = true)
    {
        List<Cell> neighbours = new List<Cell>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0) continue;

                int checkX = cell.GridX + x;
                int checkY = cell.GridY + y;

                GridInstance current = _default;
                if (!useDefaultGrid) current = _second;

                if (checkX >= 0 && checkX < current.GridSizeX
                    && checkY >= 0 && checkY < current.GridSizeY)
                    neighbours.Add(current.GridArray[checkX, checkY]);
            }
        }
        return neighbours;
    }

    /// <summary>
    /// Returns the position of an object in the <see cref="Grid"/> along the x and y axes.
    /// </summary>
    /// <param name="worldPosition">  The position of the object in the game world <see cref="Vector3"/> along the x,y and z axes.</param>
    /// <param name="useDefaultGrid"> The boolean variable specifying from which grid the nearest cells will be selected.</param>
    internal Cell GetCellFromeWorldPoint(Vector3 worldPosition, bool useDefaultGrid = true)
    {
        float percentX = Mathf.Clamp01((worldPosition.x + _gridWorldSize.x / 2) / _gridWorldSize.x);
        float percentY = Mathf.Clamp01((worldPosition.y + _gridWorldSize.y / 2) / _gridWorldSize.y);

        GridInstance current = _default;
        if (!useDefaultGrid) current = _second;

        int x = Mathf.RoundToInt((current.GridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((current.GridSizeY - 1) * percentY);

        return current.GridArray[x, y];
    }

    /// <summary>
    /// <see cref="Grid"/> rendering method.
    /// </summary>
    private void OnDrawGizmos()
    {
        Vector3 size = new Vector3(_gridWorldSize.x, _gridWorldSize.y, 1);
        Gizmos.DrawWireCube(transform.position, size);

        if (DisplayGridGizmous && _default.GridArray != null)
        {
            Cell playerCell = GetCellFromeWorldPoint(_player.position, true);
            foreach (Cell Cell in _default.GridArray)
            {
                Gizmos.color = Cell.Walkable ? Color.white : Color.red;
                if (playerCell == Cell) Gizmos.color = Color.cyan;
                Gizmos.DrawCube(Cell.WorldPosition, Vector3.one * (_default.CellDiametor - .1f));
            }
        }

        if (DisplaySecondGridGizmous && _second.GridArray != null)
        {
            Cell playerCell = GetCellFromeWorldPoint(_player.position, false);
            foreach (Cell Cell in _second.GridArray)
            {
                Gizmos.color = Cell.Walkable ? Color.white : Color.red;
                if (playerCell == Cell) Gizmos.color = Color.cyan;
                Gizmos.DrawCube(Cell.WorldPosition, Vector3.one * (_second.CellDiametor - .1f));
            }
        }
    }

    /// <summary>
    /// TerrainType this is the layer and size of the penalty in the calculation of
    /// the path cost in the pathfinding algorithm <see cref="Pathfinding"/> 
    /// for overtraveling on cells with the given TerrainType.
    /// </summary>
    [System.Serializable]
    public struct TerrainType
    {
        public LayerMask terrainMask;
        public int terrainPanalty;
    }

    [Serializable]
    /// <summary>
    /// Grid data structure;
    /// </summary>
    private struct GridInstance
    {
        /// <summary>
        /// Radius of <see cref="Cell"/>s of the <see cref="Grid"/>.
        /// </summary>
        [SerializeField] private float _cellRadius;

        /// <summary>
        /// The number of cells that the grid includes along the x axis, in the <see cref="Grid"/>.
        /// </summary>
        private int _gridSizeX;

        /// <summary>
        /// The number of cells that the grid includes along the y axis, in the <see cref="Grid"/>.
        /// </summary>
        private int _gridSizeY;

        /// <summary>
        /// <see cref="Grid"/> cell diameter.
        /// </summary>
        private float _cellDiametor;

        /// <summary>
        /// The two-dimensional array of cells representing a grid in the two-dimensional space.
        /// </summary>
        private Cell[,] _grid;

        /// <inheritdoc cref="_cellRadius"/>
        public float CellRadius
        {
            get { return _cellRadius; }
        }

        /// <inheritdoc cref="_gridSizeX"/>
        public int GridSizeX
        {
            get { return _gridSizeX; }
            set { _gridSizeX = value; }
        }

        /// <inheritdoc cref="_gridSizeY"/>
        public int GridSizeY
        {
            get { return _gridSizeY; }
            set { _gridSizeY = value; }
        }

        /// <inheritdoc cref="_cellDiametor"/>
        public float CellDiametor
        {
            get { return _cellDiametor; }
            set { _cellDiametor = value; }
        }

        /// <inheritdoc cref="_grid"/>
        public Cell[,] GridArray
        {
            get { return _grid; }
            set { _grid = value; }
        }
    }
}
