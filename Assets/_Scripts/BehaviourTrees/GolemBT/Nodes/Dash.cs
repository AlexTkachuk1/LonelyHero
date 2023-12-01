using Assets._Scripts.BehaviourTrees.Basic;
using Assets._Scripts.Models.Enums;
using UnityEngine;

public class Dash : Node
{
    /// <inheritdoc cref="Golem"/>
    private readonly Golem _npc;

    /// <summary>
    /// <see cref="Golem"/> animator component
    /// </summary>
    private readonly Animator _animator;

    public Dash(Golem nps, Animator animator)
    {
        _npc = nps;
        _animator = animator;
    }

    public override NodeStatus Tick()
    {
        if (!_npc.MakesDash)
            return NodeStatus.FAILURE;

        _animator.SetInteger(nameof(CharacterState), (int)CharacterState.Dash);

        if (_npc.PreparationComplete)
        {
            _npc.PreparationComplete = false;
            _npc.StartCoroutine(_npc.Dash());
        }

        return NodeStatus.SUCCESS;
    }
}
