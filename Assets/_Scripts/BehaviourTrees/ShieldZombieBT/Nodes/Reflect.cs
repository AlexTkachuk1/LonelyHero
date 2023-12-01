using Assets._Scripts.BehaviourTrees.Basic;
using Assets._Scripts.Models.Enums;
using Assets._Scripts.PrefabScripts.NPCs;
using UnityEngine;

/// <inheritdoc cref="Node"/>
public class Reflect : Node
{
    private readonly Animator _animator;

    private readonly ShieldZombie _npc;

    public Reflect(Animator animator, ShieldZombie npc)
    {
        _animator = animator;
        _npc = npc;
    }

    public override NodeStatus Tick()
    {
        _animator.SetInteger(nameof(CharacterState), (int)CharacterState.Block);

        _npc.StartCoroutine(_npc.StartReflecting());

        Parent.SetData(NodeContextItems.NextUsage, Time.time + _npc.Data.ReflectCooldown);

        return NodeStatus.SUCCESS;
    }
}