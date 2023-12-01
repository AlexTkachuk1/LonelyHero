using Assets._Scripts.BehaviourTrees.Basic;
using Assets._Scripts.Models.Enums;
using Assets._Scripts.Systems;
using UnityEngine;

/// <inheritdoc cref="Node"/>
public class CooldownAttack : Node
{
    /// <summary>
    /// <see cref="Bowman"/> animator component
    /// </summary>
    private readonly Animator _animator;

    /// <summary>
    /// <see cref="Bowman"/>
    /// </summary>
    private readonly Bowman _npc;

    public CooldownAttack(Bowman npc, Animator animator)
    {
        _npc = npc;
        _animator = animator;
    }

    public override NodeStatus Tick()
    {
        if (!_npc.CanAttack) return NodeStatus.FAILURE;

        _animator.SetInteger(nameof(CharacterState), (int)CharacterState.Attack);

        float direction = PlayerMovementSystem.Instance.PlayerTransform.position.x - _animator.transform.position.x;

        Helpers.Rotate(direction, _animator.transform);

        return NodeStatus.SUCCESS;
    }
}
