using Assets._Scripts.BehaviourTrees.Basic;
using Assets._Scripts.Models.Enums;
using UnityEngine;

namespace Assets._Scripts.BehaviourTrees.Skeleton.Nodes
{
    /// <inheritdoc cref="Node"/>
    public sealed class IsPlayerInRange : Node
    {
        private readonly float _attackRange;
        private readonly Transform _playerPosition;
        private readonly Transform _enemyPosition;

        public IsPlayerInRange (float attackRange, Transform playerPosition, Transform enemyPosition)
        {
            _attackRange = attackRange;
            _playerPosition = playerPosition;
            _enemyPosition = enemyPosition;
        }

        public override NodeStatus Tick()
        {
            bool inAttackRange = Vector2.Distance(_playerPosition.position, _enemyPosition.position) <= _attackRange;

            return inAttackRange ? NodeStatus.SUCCESS : NodeStatus.FAILURE;
        }
    }
}