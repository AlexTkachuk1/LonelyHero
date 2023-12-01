using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pathfinding))]
public class PathRequestManager : StaticInstance<PathRequestManager>
{
    /// <summary>
    /// Path lookup request queue.
    /// </summary>
    private readonly Queue<PathRequest> _pathRequestQueue = new Queue<PathRequest>();

    /// <summary>
    /// Query currently being executed.
    /// </summary>
    private PathRequest _currentPathRequest;

    /// <summary>
    /// An instance of the <see cref="Pathfinding"/> class.
    /// </summary>
    private Pathfinding _pathfinding;

    /// <summary>
    /// The boolean variable that stores data about whether the path search is currently in progress.
    /// </summary>
    private bool _isProcessingPath;

    /// <summary>
    /// Max count in <see cref="_pathRequestQueue"/>.
    /// </summary>
    private readonly int _maxPathRequestCount = 120;

    protected override void Awake()
    {
        base.Awake();
        _pathfinding = GetComponent<Pathfinding>();
    }

    /// <summary>
    /// Creates a new pathfinding request and then adds it to the <see cref="Queue{PathRequest}"/>.
    /// </summary>
    /// <param name="pathStart"> Position of the initial <see cref="Cell"/> in the game world.</param>
    /// <param name="pathEnd"> Position of the target <see cref="Cell"/> in the game world.</param>
    /// <param name="callback"> Delegate.</param>
    public static void ReuestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callback)
    {
        Instance.ResetPathList();

        PathRequest newRequest = new PathRequest(pathStart, pathEnd, callback);

        if (Instance._pathRequestQueue.Count > Instance._maxPathRequestCount)
            Instance._pathRequestQueue.Clear();

        Instance._pathRequestQueue.Enqueue(newRequest);
        Instance.TryProcessNext();
    }

    /// <inheritdoc cref="Pathfinding.ResetPathList"/>>
    public void ResetPathList() => _pathfinding.ResetPathList();

    /// <summary>
    /// Finishes executing the request to find the path.
    /// </summary>
    /// <param name="path"> An array of <see cref="Vector3"/>s representing the path</param>
    /// <param name="success"> The boolean variable that stores data about whether the request to find the path is successfully executed.</param>
    public void FinishedProcessingPath(Vector3[] path, bool success)
    {
        _currentPathRequest.Callback(path, success);
        _isProcessingPath = false;
        TryProcessNext();
    }

    /// <summary>
    /// Tries to start executing the next path request in the queue.
    /// </summary>
    private void TryProcessNext()
    {
        if (!_isProcessingPath && _pathRequestQueue.Count > 0)
        {
            _currentPathRequest = _pathRequestQueue.Dequeue();
            _isProcessingPath = true;
            _pathfinding.StartFindPath(_currentPathRequest.PathStart, _currentPathRequest.PathEnd);
        }
    }

    /// <summary>
    /// Path search query.
    /// </summary>
    private struct PathRequest
    {
        public Vector3 PathStart;
        public Vector3 PathEnd;
        public Action<Vector3[], bool> Callback;

        public PathRequest(Vector3 start, Vector3 end, Action<Vector3[], bool> callback)
        {
            PathStart = start;
            PathEnd = end;
            Callback = callback;
        }
    }
}
