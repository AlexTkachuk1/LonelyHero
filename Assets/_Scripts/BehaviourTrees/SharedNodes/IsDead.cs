using Assets._Scripts.BehaviourTrees.Basic;
using Assets._Scripts.Models.Base;
using Assets._Scripts.Models.Enums;
using UnityEngine;

namespace Assets._Scripts.BehaviourTrees.Skeleton.Nodes
{
    /// <inheritdoc cref="Node"/>
    public sealed class IsDead : Node
    {
        private readonly Animator _animator;

        private readonly BaseStats _stats;

        /// <summary>
        /// Root node of <see cref="BehaviourTree"/>
        /// </summary>
        private Node _root;

        public IsDead(Animator animator, BaseStats stats, Node root)
        {
            _root = root;
            _animator = animator;
            _stats = stats;
        }
        public override NodeStatus Tick()
        {
            if (_stats.IsAlive)
                return NodeStatus.FAILURE;

            _animator.SetInteger(nameof(CharacterState), (int)CharacterState.Death);

            _root = null;
            return NodeStatus.SUCCESS;
        }
    }
}