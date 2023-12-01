using System.Collections.Generic;
using Assets._Scripts.BehaviourTrees.Basic;
using Assets._Scripts.BehaviourTrees.Skeleton.Nodes;
using Assets._Scripts.PrefabScripts.NPCs;
using Assets._Scripts.Systems;
using UnityEngine;

namespace Assets._Scripts
{
    /// <inheritdoc cref="BehaviourTree"/>
    [RequireComponent(typeof(Skeleton))]
    [RequireComponent(typeof(Animator))]
    public sealed class SkeletonBT : BehaviourTree
    {
        /// <summary>
        /// <see cref="Skeleton"/> animator component
        /// </summary>
        private Animator _animator;

        /// <inheritdoc cref="Skeleton"/>
        private Skeleton _skeleton;

        /// <inheritdoc cref="BehaviourTree.SetupTree"/>
        protected override void Start()
        {
            _animator = GetComponent<Animator>();
            _skeleton = GetComponent<Skeleton>();

            base.Start();
        }

        /// <inheritdoc cref="BehaviourTree.SetupTree"/>
        protected override Node SetupTree()
        {
            _root = new Selector(new List<Node>
            {
                new IsDead(_animator , _skeleton.RuntimeData.BaseStats , _root),
                new Sequence(new List<Node>
                {
                    new IsPlayerInRange(_skeleton.Data.AttackRange,PlayerMovementSystem.Instance.PlayerTransform,transform),
                    new Attack( _animator)
                }),
                new WalkTo(_animator)
            });

            return _root;
        }
    }
}