using Assets._Scripts.BehaviourTrees.Basic;
using Assets._Scripts.Models.Enums;
using UnityEngine;

/// <inheritdoc cref="Node"/>
public class Explosion : Node
{
    private readonly Animator _animator;
    public Explosion(Animator animator)
    {
        _animator = animator;
    }
    public override NodeStatus Tick()
    {
        _animator.SetInteger(nameof(CharacterState), (int)CharacterState.Attack);
        return NodeStatus.SUCCESS;
    }
}
