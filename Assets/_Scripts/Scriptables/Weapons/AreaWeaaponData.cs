using UnityEngine;

namespace Assets._Scripts.Scriptable
{
    /// <inheritdoc cref="WeaponData"/>
    [CreateAssetMenu(fileName = "New area weapon", menuName = "Area_Weaapon")]
    public sealed class AreaWeaaponData : WeaponData
    {
        /// <summary>
        /// Attack ñooldown.
        /// <summary>
        [SerializeField] private int _attackCooldown;

        /// <summary>
        /// Attack range.
        /// </summary>
        [SerializeField] private float _attackRange;

        /// <summary>
        /// Enemy layer.
        /// </summary>
        [SerializeField] private LayerMask _enemyLayer;

        /// <inheritdoc cref="_attackCooldown"/>
        public int AttackCooldown
        {
            get => _attackCooldown;

            set
            {
                if (_attackCooldown < 0)
                    _attackCooldown = 0;

                _attackCooldown = value;
            }
        }

        /// <inheritdoc cref="_attackRange"/>
        public float AttackRange
        {
            get => _attackRange;

            set
            {
                if (_attackRange < 0)
                    _attackRange = 0;

                _attackRange = value;
            }
        }

        /// <inheritdoc cref="_enemyLayer"/>
        public LayerMask EnemyLayer
        {
            get => _enemyLayer;
        }
    }
}