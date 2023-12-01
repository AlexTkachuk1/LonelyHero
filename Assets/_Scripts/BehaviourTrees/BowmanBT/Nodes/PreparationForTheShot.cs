using Assets._Scripts.BehaviourTrees.Basic;
using Assets._Scripts.Models.Enums;
using Assets._Scripts.Systems;
using UnityEngine;

public class PreparationForTheShot : Node
{
    /// <inheritdoc cref="Bowman"/>
    private readonly Bowman _npc;

    /// <summary>
    /// <see cref="Bowman"/> animator component
    /// </summary>
    private readonly Animator _animator;

    public PreparationForTheShot(Bowman nps, Animator animator)
    {
        _npc = nps;
        _animator = animator;
    }

    public override NodeStatus Tick()
    {
        if (_npc.CanAttack)
            return NodeStatus.FAILURE;

        _animator.SetInteger(nameof(CharacterState), (int)CharacterState.Idle);
        _npc.StartAttackRecharg();

        float direction = PlayerMovementSystem.Instance.PlayerTransform.position.x - _animator.transform.position.x;

        Helpers.Rotate(direction, _animator.transform);

        return NodeStatus.SUCCESS;
    }
}
