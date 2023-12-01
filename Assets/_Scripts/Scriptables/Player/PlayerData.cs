using System.Collections.Generic;
using Assets._Scripts.Models.Base;
using UnityEngine;

namespace Assets._Scripts.Scriptable.Player
{
    /// <summary>
    /// Scriptable object for <see cref="Units.Player.Player"/>
    /// </summary>
    [CreateAssetMenu(fileName = "New player data")]
    public sealed class PlayerData : ScriptableObject
    {
        /// <inheritdoc cref="Models.Base.BaseStats"/>
        public BaseStats BaseStats;

        /// <summary>
        /// Player expirience
        /// </summary>
        public float Experience;

        /// <summary>
        /// Collection of <inheritdoc cref="Resistance"/>
        /// </summary>
        [SerializeField] private Resistance[] _resistances;

        /// <inheritdoc cref="_resistances"/>
        public IEnumerable<Resistance> Resistances { get => _resistances; }
    }
}