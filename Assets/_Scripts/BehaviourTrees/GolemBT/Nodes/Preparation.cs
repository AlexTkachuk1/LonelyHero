using Assets._Scripts.BehaviourTrees.Basic;
using Assets._Scripts.Models.Enums;
using Assets._Scripts.Systems;
using UnityEngine;
public sealed class Preparation : Node
{
    /// <summary>
    /// Distance at which preparation for a dash begins.
    /// </summary>
    private readonly float _preparationRange;

    /// <summary>
    /// The minimum distance at which preparation for a dash can begin.
    /// </summary>
    private readonly float _minDashRange;

    /// <inheritdoc cref="Golem"/>
    private readonly Golem _npc;

    /// <summary>
    /// <see cref="Golem"/> animator component
    /// </summary>
    private readonly Animator _animator;

    public Preparation(float preparationRange, float minDashRange, Golem nps, Animator animator)
    {
        _preparationRange = preparationRange;
        _npc = nps;
        _animator = animator;
        _minDashRange = minDashRange;
    }

    public override NodeStatus Tick()
    {
        Vector2 playerPosition = PlayerMovementSystem.Instance.PlayerTransform.position;
        Vector2 npcPosition = (Vector2)_npc.transform.position;
        Vector2 playerDirection = playerPosition - npcPosition;

        float squareDistance = (playerPosition - npcPosition).sqrMagnitude;
        bool playerWithinDashRadius = squareDistance <= _preparationRange * _preparationRange;
        bool minimumDashRadius = squareDistance >= _minDashRange * _minDashRange;

        RaycastHit2D hit = Physics2D.Raycast(npcPosition, playerDirection, _npc.Data.DashRange, _npc.Data.TargetLayer);

        if (_npc.MakesDash || !minimumDashRadius || !playerWithinDashRadius || hit.collider == null)
            return NodeStatus.FAILURE;

        _animator.SetInteger(nameof(CharacterState), (int)CharacterState.Preparation);

        if (!_npc.PreparationComplete)
            _npc.StartDashPreparation();

        return NodeStatus.SUCCESS;
    }
}
