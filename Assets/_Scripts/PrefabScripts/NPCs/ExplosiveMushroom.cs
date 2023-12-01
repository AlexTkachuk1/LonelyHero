using Assets._Scripts.Models.Base;
using Assets._Scripts.Scriptable.NPCs.Base;
using Assets._Scripts.Systems.NPCs;
using UnityEngine;

/// <inheritdoc cref="NPC"/>
public sealed class ExplosiveMushroom : NPC<NpcData>, IDamageable
{

    /// <inheritdoc cref="IDamageable.TakeDamage(DamageStats)"/>
    public override float TakeDamage(DamageStats damageStats)
    {
        NpcDamageSystem.Instance.ApplyDamage(RuntimeData.BaseStats, damageStats, Data.Resistances, out int damageTaken);
        UIController.Instance.OnTakeDamage(transform.position, damageTaken);
        return damageTaken;
    }
}