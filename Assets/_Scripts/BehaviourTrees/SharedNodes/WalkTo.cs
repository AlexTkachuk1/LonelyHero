using Assets._Scripts.BehaviourTrees.Basic;
using Assets._Scripts.Models.Enums;
using Assets._Scripts.Systems;
using UnityEngine;

namespace Assets._Scripts.BehaviourTrees.Skeleton.Nodes
{
    /// <inheritdoc cref="Node"/>
    public sealed class WalkTo : Node
    {
        private readonly Animator _animator;
        public WalkTo(Animator animator) => _animator = animator;
        public override NodeStatus Tick()
        {
            _animator.SetInteger(nameof(CharacterState), (int)CharacterState.Walk);

            float direction = PlayerMovementSystem.Instance.PlayerTransform.position.x - _animator.transform.position.x;

            Helpers.Rotate(direction, _animator.transform);

            return NodeStatus.SUCCESS;
        }
    }
}