using System;
using UnityEngine;

namespace Assets._Scripts.Models.Base
{
    /// <summary>
    /// Base stats
    /// </summary>
    [Serializable]
    public class BaseStats
    {
        public BaseStats(){}

        public BaseStats(BaseStats baseStats)
        {
            Speed = baseStats.Speed;
            Health = baseStats.Health;
            _maxHealth = baseStats.MaxHealth;
        }

        public float Speed;

        public float Health;

        [SerializeField] private float _maxHealth;

        public float MaxHealth => _maxHealth;

        public bool IsAlive { get => Health > 0; }
    }
}