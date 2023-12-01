using Assets._Scripts.Models.Base;
using Assets._Scripts.Scriptable.NPCs.Base;
using Assets._Scripts.Systems.NPCs;
using UnityEngine;

namespace Assets._Scripts.PrefabScripts.NPCs
{
    /// <inheritdoc cref="NPC{T}"/>
    public sealed class Skeleton : NPC<NpcData>, IDamageable
    {
        /// <inheritdoc cref="IDamageable.TakeDamage(DamageStats)"/>
        public override float TakeDamage(DamageStats damageStats)
        {
            NpcDamageSystem.Instance.ApplyDamage(RuntimeData.BaseStats, damageStats, Data.Resistances, out int damageTaken);
            UIController.Instance.OnTakeDamage(transform.position, damageTaken);
            return damageTaken;
        }
    }
}