using System.Collections.Generic;
using Assets._Scripts.BehaviourTrees.Basic;
using Assets._Scripts.BehaviourTrees.Skeleton.Nodes;
using Assets._Scripts.Systems;
using UnityEngine;

namespace Assets._Scripts
{
    /// <inheritdoc cref="BehaviourTree"/>
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(ExplosiveMushroom))]
    public sealed class ExplosiveMushroomBT : BehaviourTree
    {
        /// <summary>
        /// <see cref="ExplosiveMushroom"/> animator component
        /// </summary>
        private Animator _animator;

        /// <inheritdoc cref="ExplosiveMushroom"/>
        private ExplosiveMushroom _npc;

        /// <inheritdoc cref="BehaviourTree.SetupTree"/>
        protected override void Start()
        {
            _animator = GetComponent<Animator>();
            _npc = gameObject.GetComponent<ExplosiveMushroom>();

            base.Start();
        }

        /// <inheritdoc cref="BehaviourTree.SetupTree"/>
        protected override Node SetupTree()
        {
            _root = new Selector(new List<Node>
                {
                    new IsDead(_animator ,  _npc.RuntimeData.BaseStats , _root),
                    new Sequence(new List<Node>
                    {
                        new IsPlayerInRange(_npc.Data.AttackRange,PlayerMovementSystem.Instance.PlayerTransform,transform),
                        new Explosion(_animator)
                    }),
                 new WalkTo(_animator)
                });

            return _root;
        }
    }
}