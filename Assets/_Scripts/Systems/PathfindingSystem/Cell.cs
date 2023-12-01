using UnityEngine;

/// <summary>
/// The entity that provides the cell in the pathfinding algorithm <see cref="Pathfinding"/>.
/// </summary>
public class Cell : IHeapItem<Cell>
{
    /// <summary>
    /// The cell that is the parent of this cell in the heap structure <see cref="Heap{T}"/>.
    /// </summary>
    public Cell Parent;

    /// <summary>
    /// A boolean value indicating whether the cell is an obstacle.
    /// </summary>
    public bool Walkable;

    /// <summary>
    /// Ñell position.
    /// </summary>
    private Vector3 _worldPosition;

    /// <summary>
    /// The sum of the penalty in the pathfinding algorithm <see cref="Pathfinding"/> for moving around this cell.
    /// </summary>
    private int _movmentPenalty;

    /// <summary>
    /// The number of current cell in the heap structure <see cref="Heap{T}"/>.
    /// </summary>
    private int _heapIndex;

    /// <summary>
    /// The cost of the path from the starting cell in the pathfinding algorithm <see cref="Pathfinding"/> to the current cell.
    /// </summary>
    private int _gCost;

    /// <summary>
    /// The cost of the path from the current cell in the pathfinding algorithm <see cref="Pathfinding"/> to the target cell.
    /// </summary>
    private int _hCost;

    public Cell(bool walkable, Vector3 worldPosition, int gridX, int gridY, int penalty)
    {
        Walkable = walkable;
        GridX = gridX;
        GridY = gridY;

        _worldPosition = worldPosition;
        _movmentPenalty = penalty;
    }

    /// <summary>
    /// X-axis coordinate in the grid <see cref="Grid"/>.
    /// </summary>
    public int GridX { get; private set; }

    /// <summary>
    /// Y-axis coordinate in the grid <see cref="Grid"/>.
    /// </summary>
    public int GridY { get; private set; }

    /// <inheritdoc cref="_worldPosition"/>
    public Vector3 WorldPosition
    {
        get { return _worldPosition; }
        set { _worldPosition = value; }
    }

    /// <inheritdoc cref="_gCost"/>
    public int GCost
    {
        get { return _gCost; }
        set
        {
            if (value > 0)
                _gCost = value;
        }
    }

    /// <inheritdoc cref="_hCost"/>
    public int HCost
    {
        get { return _hCost; }
        set
        {
            if (value > 0)
                _hCost = value;
        }
    }

    /// <summary>
    /// The cost of the path from the start cell in the pathfinding algorithm <see cref="Pathfinding"/> to the target cell.
    /// </summary>
    public int FCost
    {
        get { return _gCost + _hCost; }
    }

    /// <inheritdoc cref="_movmentPenalty"/>
    public int MovmentPenalty
    {
        get { return _movmentPenalty; }
        set
        {
            if (value > 0)
                _movmentPenalty = value;
        }
    }

    /// <inheritdoc cref="_heapIndex"/>
    public int HeapIndex
    {
        get
        {
            return _heapIndex;
        }
        set
        {
            _heapIndex = value;
        }
    }

    /// <summary>
    /// The method compares the values of the absolute path from the starting cell <see cref="Cell"/> to
    /// the target cell <see cref="Cell"/> through the current cell <see cref="Cell"/> and cellToCompare
    /// and returns 1 if the cost of the path through the current cell is less than the path through
    /// cellToCompare, if the cost of the paths is equal, the cost of the paths from the current cell to the
    /// target and from cellToCompare to the target are compared.
    /// </summary>
    /// <param name="cellToCompare"> Cell <see cref="Cell"/> to compare.</param>

    public int CompareTo(Cell cellToCompare)
    {
        int compare = FCost.CompareTo(cellToCompare.FCost);

        if (compare == 0)
            compare = HCost.CompareTo(cellToCompare.HCost);

        return -compare;
    }
}
