using System.Collections.Generic;
using Assets._Scripts.BehaviourTrees.Basic;
using Assets._Scripts.BehaviourTrees.Skeleton.Nodes;
using Assets._Scripts.Systems;
using UnityEngine;

/// <inheritdoc cref="BehaviourTree"/>
[RequireComponent(typeof(Golem))]
[RequireComponent(typeof(Animator))]
public class GolemBT : BehaviourTree
{
    /// <summary>
    /// <see cref="Golem"/> animator component
    /// </summary>
    private Animator _animator;

    /// <inheritdoc cref="Golem"/>
    private Golem _golem;

    /// <inheritdoc cref="BehaviourTree.SetupTree"/>
    protected override void Start()
    {
        _animator = GetComponent<Animator>();
        _golem = GetComponent<Golem>();

        base.Start();
    }

    /// <inheritdoc cref="BehaviourTree.SetupTree"/>
    protected override Node SetupTree()
    {
        _root = new Selector(new List<Node>
            {
                new IsDead(_animator , _golem.RuntimeData.BaseStats , _root),
                new Dash(_golem, _animator),
                new Sequence(new List<Node>
                {
                    new IsPlayerInRange(_golem.Data.AttackRange,PlayerMovementSystem.Instance.PlayerTransform,transform),
                    new Attack( _animator)
                }),
                new Preparation(_golem.Data.PreparationRange, _golem.Data.MinDashRange, _golem, _animator),
                new GolemWalkTo(_animator, _golem)
            });

        return _root;
    }
}