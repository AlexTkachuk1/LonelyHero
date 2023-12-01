using System;
using Assets._Scripts.Models.Enums;

namespace Assets._Scripts.Models.Base
{
    /// <summary>
    /// Structure that represent damage stats
    /// </summary>
    [Serializable]
    public struct DamageStats
    {
        /// <summary>
        /// Damage amount stat
        /// </summary>
        public float Amount;

        /// <inheritdoc cref="DamageType"/>
        public DamageType Type;
    }
}
