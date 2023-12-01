using System.Collections.Generic;
using Assets._Scripts.BehaviourTrees.Basic;
using Assets._Scripts.BehaviourTrees.Skeleton.Nodes;
using Assets._Scripts.Systems;
using UnityEngine;
public class BowmanBT : BehaviourTree
{
    /// <summary>
    /// <see cref="Golem"/> animator component
    /// </summary>
    private Animator _animator;

    /// <inheritdoc cref="Bowman"/>
    private Bowman _bowman;

    protected override void Start()
    {
        _animator = GetComponent<Animator>();
        _bowman = GetComponent<Bowman>();

        base.Start();
    }

    /// <inheritdoc cref="BehaviourTree.SetupTree"/>
    protected override Node SetupTree()
    {
        _root = new Selector(new List<Node>
            {
                new IsDead(_animator , _bowman.RuntimeData.BaseStats , _root),
                new Sequence(new List<Node>
                {
                    new IsPlayerInRange(_bowman.Data.AttackRange,PlayerMovementSystem.Instance.PlayerTransform,transform),
                    new CooldownAttack(_bowman, _animator),

                }),
                new PreparationForTheShot(_bowman, _animator),
                new WalkTo(_animator),
            });

        return _root;
    }
}
