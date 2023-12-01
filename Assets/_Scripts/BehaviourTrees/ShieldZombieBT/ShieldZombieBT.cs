using System.Collections.Generic;
using Assets._Scripts.BehaviourTrees.Basic;
using Assets._Scripts.BehaviourTrees.SharedNodes;
using Assets._Scripts.BehaviourTrees.Skeleton.Nodes;
using Assets._Scripts.PrefabScripts.NPCs;
using Assets._Scripts.Systems;
using UnityEngine;

namespace Assets._Scripts
{
    /// <inheritdoc cref="BehaviourTree"/>
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(ShieldZombieBT))]
    public sealed class ShieldZombieBT : BehaviourTree
    {
        /// <summary>
        /// <see cref="ShieldZombie"/> animator component
        /// </summary>
        private Animator _animator;

        /// <inheritdoc cref="ShieldZombie"/>
        private ShieldZombie _shieldZombie;

        /// <inheritdoc cref="BehaviourTree.SetupTree"/>
        protected override void Start()
        {
            _animator = GetComponent<Animator>();
            _shieldZombie = GetComponent<ShieldZombie>();

            base.Start();
        }

        /// <inheritdoc cref="BehaviourTree.SetupTree"/>
        protected override Node SetupTree()
        {
            _root = new Selector(new List<Node>
            {
                new IsDead(_animator , _shieldZombie.RuntimeData.BaseStats , _root),
                new IsReflecting(_shieldZombie),

                new Sequence(new List<Node>
                {
                    new IsOnCooldown(),
                    new Reflect(_animator , _shieldZombie),
                }),
                new Sequence(new List<Node>
                {
                    new IsPlayerInRange(_shieldZombie.Data.AttackRange,PlayerMovementSystem.Instance.PlayerTransform,transform),
                    new Attack( _animator)
                }),
                new WalkTo(_animator)
            });

            return _root;
        }
    }
}