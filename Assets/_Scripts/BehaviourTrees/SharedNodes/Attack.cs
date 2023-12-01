using Assets._Scripts.BehaviourTrees.Basic;
using Assets._Scripts.Models.Enums;
using UnityEngine;

namespace Assets._Scripts.BehaviourTrees.Skeleton.Nodes
{
    /// <inheritdoc cref="Node"/>
    public sealed class Attack : Node
    {
        private readonly Animator _animator;
        public Attack(Animator animator) => _animator = animator;

        public override NodeStatus Tick()
        {
            _animator.SetInteger(nameof(CharacterState), (int)CharacterState.Attack);
            return NodeStatus.SUCCESS;
        }
    }
}