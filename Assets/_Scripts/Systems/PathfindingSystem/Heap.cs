using System;

public class Heap<T> where T : IHeapItem<T>
{
    /// <summary>
    /// Array of <see cref="Heap{T}"/> elements of type <see cref="T"/>.
    /// </summary>
    private readonly T[] _items;


    /// <summary>
    /// Number of _items in the <see cref="Heap{T}"/>.
    /// </summary>
    private int currentItemCount;

    public Heap(int maxHeapSize)
    {
        _items = new T[maxHeapSize];
    }

    /// <inheritdoc cref="currentItemCount"/>
    public int Count
    {
        get { return currentItemCount; }
    }

    /// <summary>
    /// Adds an item to the <see cref="Heap{T}"/>.
    /// </summary>
    /// <param name="item">  Object of type <see cref="T"/>.</param>
    public void Add(T item)
    {
        item.HeapIndex = currentItemCount;
        _items[currentItemCount] = item;
        SortUp(item);
        currentItemCount++;
    }

    /// <summary>
    /// Removes the first element of the <see cref="Heap{T}"/>.
    /// </summary>
    public T RemoveFirst()
    {
        T firstItem = _items[0];
        currentItemCount--;
        _items[0] = _items[currentItemCount];
        _items[0].HeapIndex = 0;
        SortDown(_items[0]);
        return firstItem;
    }

    /// <summary>
    /// Sorts the item in the <see cref="Heap{T}"/> in ascending order,
    /// swapping it with the parent if its priority is higher.
    /// </summary>
    /// <param name="item">  Object of type <see cref="T"/>.</param>
    public void UpdateItem(T item)
    {
        SortUp(item);
    }

    /// <summary>
    /// Ñhecks if there is such an item in the <see cref="Heap{T}"/>.
    /// </summary>
    /// <param name="item">  Object of type <see cref="T"/>.</param>
    public bool Contains(T item)
    {
        return Equals(_items[item.HeapIndex], item);
    }

    /// <summary>
    /// Sorts the item in the <see cref="Heap{T}"/> in descending order,
    /// swapping it with child _items if its priority is lower.
    /// </summary>
    /// <param name="item">  Object of type <see cref="T"/>.</param>
    private void SortDown(T item)
    {
        int swapIndex = 0;
        int childIndexLeft = item.HeapIndex * 2 + 1;
        int childIndexRight = item.HeapIndex * 2 + 2;

        if (childIndexLeft < currentItemCount)
        {
            swapIndex = childIndexLeft;

            if (childIndexRight < currentItemCount
                && _items[childIndexLeft].CompareTo(_items[childIndexRight]) < 0)
                swapIndex = childIndexRight;

            if (item.CompareTo(_items[swapIndex]) < 0)
            {
                Swap(item, _items[swapIndex]);
                SortDown(item);
            }
        }
    }

    /// <summary>
    /// Sorts the item in the <see cref="Heap{T}"/> in ascending order,
    /// swapping it with the parent if its priority is higher.
    /// </summary>
    /// <param name="item">  Object of type <see cref="T"/>.</param>
    private void SortUp(T item)
    {
        int parentIndex = GetParentIndex(item);

        T patentItem = _items[parentIndex];

        if (item.CompareTo(patentItem) > 0)
        {
            Swap(item, patentItem);
            SortUp(item);
        }
    }

    /// <summary>
    /// Returns the index of the parent item in the <see cref="Heap{T}"/>.
    /// </summary>
    /// <param name="item">  Object of type <see cref="T"/>.</param>
    private int GetParentIndex(T item)
    {
        return (item.HeapIndex - 1) / 2;
    }

    /// <summary>
    /// Swaps the positions of _items A and B in the <see cref="Heap{T}"/> structure.
    /// </summary>
    /// <param name="itemA">  Object of type <see cref="T"/>.</param>
    /// <param name="itemB">  Object of type <see cref="T"/>.</param>
    private void Swap(T itemA, T itemB)
    {
        _items[itemA.HeapIndex] = itemB;
        _items[itemB.HeapIndex] = itemA;
        int itemAIndex = itemA.HeapIndex;
        itemA.HeapIndex = itemB.HeapIndex;
        itemB.HeapIndex = itemAIndex;
    }
}

public interface IHeapItem<T> : IComparable<T>
{
    public int HeapIndex { get; set; }
}
