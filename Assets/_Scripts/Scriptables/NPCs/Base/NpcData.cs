using System.Collections.Generic;
using Assets._Scripts.Models.Base;
using UnityEngine;

namespace Assets._Scripts.Scriptable.NPCs.Base
{
    /// <summary>
    /// Scriptable object for <see cref="NPC{T}"/>
    /// </summary>
    [CreateAssetMenu(fileName = "New NPC data")]
    public class NpcData : ScriptableObject
    {
        /// <inheritdoc cref="Models.Base.BaseStats"/>
        [SerializeField] private BaseStats _baseStats;

        /// <summary>
        /// <see cref="NPC{T}"/> attack range
        /// </summary>
        [SerializeField] private float _attackRange;

        /// <summary>
        /// <see cref="NPC{T}"/> experience gain
        /// </summary>
        [SerializeField] private float _experience;

        /// <inheritdoc cref="Models.Base.DamageStats"/>
        [SerializeField] private DamageStats _damageStats;

        /// <inheritdoc cref="Resistance"/>
        [SerializeField] private Resistance[] _resistances;

        /// <inheritdoc cref="_attackRange"/>
        public float AttackRange { get => _attackRange; }

        /// <inheritdoc cref="_experience"/>
        public float Experience { get => _experience; }

        /// <inheritdoc cref="_baseStats"/>
        public BaseStats BaseStats { get => _baseStats; }

        /// <inheritdoc cref="_damageStats"/>
        public DamageStats DamageStats { get => _damageStats; }

        /// <inheritdoc cref="_resistances"/>
        public IEnumerable<Resistance> Resistances { get => _resistances; }

    }
}