using System.Collections.Generic;
using Assets._Scripts.Models.Base;
using Assets._Scripts.Scriptable.NPCs.Base;
using UnityEngine;

namespace Assets._Scripts.Systems.Player
{
    /// <summary>
    /// System that manages <see cref="DamageStats"/> and <see cref="Resistance"/> and <see cref="BaseStats"/> of other <see cref="NPC{T}"/> or <see cref="Units.Player.Player"/>
    /// </summary>
    public sealed class PlayerDamageSystem : Singleton<PlayerDamageSystem>
    {
        /// <summary>
        /// Appy damage that caused by enemy to player
        /// </summary>
        /// <param name="damageStats"><inheritdoc cref="NpcData.DamageStats"/></param>
        /// <returns><see cref="BaseStats"/></returns>
        public BaseStats ApplyDamage(BaseStats playerStats, DamageStats damageStats, IEnumerable<Resistance> resistances , out int damageTaken)
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

            playerStats.Health -= damageTaken;
         
            return playerStats;
        }
    }
}