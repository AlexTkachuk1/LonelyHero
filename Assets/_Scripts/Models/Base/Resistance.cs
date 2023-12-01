using System;
using Assets._Scripts.Models.Enums;

namespace Assets._Scripts.Models.Base
{
    /// <summary>
    /// Resistance stats. 
    /// </summary>
    [Serializable]
    public struct Resistance
    {
        /// <summary>
        /// Amount of resistance ( Decrease attack on this percent or amount )
        /// </summary>
        public float Amount;

        /// <inheritdoc cref=" DamageType"/>
        public DamageType Type;
    }
}
