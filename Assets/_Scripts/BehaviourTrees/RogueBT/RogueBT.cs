using System.Collections.Generic;
using Assets._Scripts.BehaviourTrees.Basic;
using Assets._Scripts.BehaviourTrees.SharedNodes;
using Assets._Scripts.BehaviourTrees.Skeleton.Nodes;
using Assets._Scripts.PrefabScripts.NPCs;
using Assets._Scripts.Systems;
using UnityEngine;

/// <inheritdoc cref="BehaviourTree"/>
[RequireComponent(typeof(Rogue))]
[RequireComponent(typeof(Animator))]
public sealed class RogueBT : BehaviourTree
{
    /// <summary>
    /// <see cref="Rogue"/> animator component
    /// </summary>
    private Animator _animator;

    /// <inheritdoc cref="Rogue"/>
    private Rogue _rogue;

    /// <inheritdoc cref="BehaviourTree.SetupTree"/>
    protected override void Start()
    {
        _animator = GetComponent<Animator>();
        _rogue = GetComponent<Rogue>();

        base.Start();
    }

    /// <inheritdoc cref="BehaviourTree.SetupTree"/>
    protected override Node SetupTree()
    {
        _root = new Selector(new List<Node>
            {
                new IsDead(_animator , _rogue.RuntimeData.BaseStats , _root),
                new Sequence(new List<Node>
                {
                    new IsOnCooldown(),
                    new Evade(_animator,_rogue),
                }),
                new Sequence(new List<Node>
                {
                    new IsPlayerInRange(_rogue.Data.AttackRange,PlayerMovementSystem.Instance.PlayerTransform,transform),
                    new Attack( _animator)
                }),

                new WalkTo(_animator)
            });

        return _root;
    }
}