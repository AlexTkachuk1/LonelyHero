using System.Collections.Generic;
using Assets._Scripts.Models.Base;
using Assets._Scripts.Scriptable;
using Assets._Scripts.Scriptable.NPCs.Base;
using UnityEngine;

namespace Assets._Scripts.Systems.NPCs
{
    /// <summary>
    /// System that responsible for managing damage applied to enemy
    /// </summary>
    public class NpcDamageSystem : Singleton<NpcDamageSystem>
    {
        /// <summary>
        /// Apply damage that caused by weapon to enemy
        /// </summary>
        /// <param name="stats"><inheritdoc cref="BaseStats"/></param>
        /// <param name="resistances"><inheritdoc cref="NpcData.Resistances"/></param>
        /// <param name="damageStats"><inheritdoc cref="WeaponData.Stats"/></param>
        /// <returns><see cref="BaseStats"/></returns>
        public BaseStats ApplyDamage(BaseStats stats, DamageStats damageStats, IEnumerable<Resistance> resistances , out int damageTaken)
        {
            float damage = damageStats.Amount;

            foreach (Resistance resistance in resistances)
            {
                if (resistance.Type == damageStats.Type)
                {
                    damage -= damage * resistance.Amount / 100;
                    break;
                }
            }

            damageTaken = Mathf.RoundToInt(damage);

            stats.Health -= damageTaken;

            return stats;
        }
    }
}