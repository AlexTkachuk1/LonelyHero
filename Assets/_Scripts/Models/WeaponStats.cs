using System;

namespace Assets._Scripts.Models.Base
{
    /// <summary>
    /// Weapon stats
    /// </summary>
    [Serializable]
    public struct WeaponStats
    {
        /// <summary>
        /// Cooldown which defines animation playtime and collider being active
        /// </summary>
        public float Countdown;

        /// DamageMultiplier for <inheritdoc cref=" WeaponData"/>
        public float Multiplier;

        /// <inheritdoc cref="Scriptable.Base.DamageStats"/>
        public DamageStats DamageStats;

        /// <summary>
        /// Appearing range of this weapon 
        /// </summary>  
        public float PositionRange;

        /// <summary>
        /// Quantity of spawning weapons
        /// </summary>
        public int CastQuantity;
    }
}