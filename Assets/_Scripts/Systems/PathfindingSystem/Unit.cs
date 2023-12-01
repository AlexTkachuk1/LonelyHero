using System.Collections;
using Assets._Scripts.Models.Enums;
using Assets._Scripts.Systems;
using UnityEngine;

[RequireComponent(typeof(IBase))]
[RequireComponent(typeof(Animator))]
public class Unit : MonoBehaviour
{
    /// <summary>
    /// Time period between path updates.
    /// </summary>
    [SerializeField] protected float _pathUpdateDelay = 0.8f;

    /// <summary>
    /// Target <see cref="Transform"/>
    /// </summary>
    protected Transform _target;

    /// <summary>
    /// Target <see cref="Animator"/>
    /// </summary>
    protected Animator _animator;

    /// <summary>
    /// An array of <see cref="Vector3"/>s representing the path to the <see cref="_target"/>.
    /// </summary>
    protected Vector3[] _path;

    /// <summary>
    /// Index of the <see cref="Vector3"/> in the <see cref="_path"/> of the path to which the move is made.
    /// </summary>
    protected int _targetIndex;

    /// <inheritdoc cref="NPC"/>
    protected IBase _npc;

    private void Start()
    {
        _npc = GetComponent<IBase>();
        _animator = GetComponent<Animator>();
        _target = PlayerMovementSystem.Instance.PlayerTransform;
        StartCoroutine(UpdatePathToTarget());
    }

    /// <summary>
    /// Launches a coroutine following the found path.
    /// </summary>
    /// <param name="path"> An array of <see cref="Vector3"/>s representing the path to the <see cref="Unit._target"/>.</param>
    /// <param name="pathSuccessful"> The boolean variable that stores data about whether the request to find the path is successfully executed.</param>
    public void OnPathFound(Vector3[] path, bool pathSuccessful)
    {
        if (this != null && pathSuccessful)
        {
            _path = path;
            StopCoroutine(nameof(FollowPath));
            StartCoroutine(nameof(FollowPath));
        }
    }

    /// <summary>
    /// Creates a new pathfinding request.
    /// </summary>
    protected IEnumerator UpdatePathToTarget()
    {
        while (true)
        {
            yield return new WaitForSeconds(_pathUpdateDelay);
            PathRequestManager.ReuestPath(transform.position, _target.position, OnPathFound);
        }
    }

    /// <summary>
    /// Method that starts sequential movement to points in the array of the found path to the target.
    /// </summary>
    protected virtual IEnumerator FollowPath()
    {
        float currentSpeed = _npc.RuntimeData.BaseStats.Speed;

        if (_animator.GetInteger(nameof(CharacterState)) != (int)CharacterState.Walk) currentSpeed = 0f;

        if (_path.Length > 0)
        {
            Vector3 currentWaypoint = _path[0];
            while (true)
            {
                if (transform.position == currentWaypoint)
                {
                    _targetIndex++;
                    if (_targetIndex >= _path.Length)
                    {
                        yield break;
                    }
                    currentWaypoint = _path[_targetIndex];
                }

                if (_npc.RuntimeData.BaseStats.IsAlive)
                    transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, currentSpeed * Time.deltaTime);

                yield return null;
            }
        }
    }
}