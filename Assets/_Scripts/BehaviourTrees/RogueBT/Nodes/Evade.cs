using Assets._Scripts.BehaviourTrees.Basic;
using Assets._Scripts.Models.Enums;
using Assets._Scripts.PrefabScripts.NPCs;
using Assets._Scripts.Systems;
using UnityEngine;

/// <inheritdoc cref="Node"/>
public class Evade : Node
{
    private readonly Animator _animator;

    private readonly Rogue _npc;

    public Evade(Animator animator, Rogue npc)
    {
        _animator = animator;
        _npc = npc;
    }

    public override NodeStatus Tick()
    {
        Collider2D[] weapons = Physics2D.OverlapCircleAll(_npc.transform.position, _npc.Data.DashRadius, _npc.Data.EvasionTargets);

        if (weapons.Length > 0)
        {
            Vector3 evadeDirection = Vector3.zero;

            foreach (Collider2D weapon in weapons)
            {
                evadeDirection += _npc.transform.position - weapon.transform.position;
            }

            evadeDirection /= weapons.Length;

            ///Make NPC dash in player direction
            evadeDirection = (PlayerMovementSystem.Instance.PlayerTransform.position + evadeDirection).normalized;

            _animator.SetInteger(nameof(CharacterState), (int)CharacterState.Dash);

            _npc.StartCoroutine(_npc.Dash(evadeDirection));

            Parent.SetData(NodeContextItems.NextUsage, Time.time + _npc.Data.DashCooldown);
        }

        return NodeStatus.SUCCESS;
    }
}