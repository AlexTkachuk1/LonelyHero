using Assets._Scripts.BehaviourTrees.Basic;
using Assets._Scripts.Models.Enums;
using Assets._Scripts.Systems;
using UnityEngine;

public class GolemWalkTo : Node
{
    /// <inheritdoc cref="Golem"/>
    private readonly Golem _npc;

    /// <summary>
    /// <see cref="Golem"/> animator component
    /// </summary>
    private readonly Animator _animator;

    public GolemWalkTo(Animator animator, Golem npc)
    {
        _animator = animator;
        _npc = npc;
    }

    public override NodeStatus Tick()
    {
        if (!_npc.PreparationComplete)
            _animator.SetInteger(nameof(CharacterState), (int)CharacterState.Walk);

        float direction = PlayerMovementSystem.Instance.PlayerTransform.position.x - _npc.transform.position.x;

        Helpers.Rotate(direction, _animator.transform);

        return NodeStatus.SUCCESS;
    }
}
