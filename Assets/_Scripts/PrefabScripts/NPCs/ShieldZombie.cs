using System.Collections;
using Assets._Scripts.Models.Base;
using Assets._Scripts.Scriptables.NPCs;
using Assets._Scripts.Systems.NPCs;
using UnityEngine;

namespace Assets._Scripts.PrefabScripts.NPCs
{
    /// <inheritdoc cref="NPC{T}"/>
    public class ShieldZombie : NPC<ReflectorData>, IDamageable
    {
        /// <summary>
        /// Reflection logic flag
        /// </summary>
        public bool IsReflecting { get; private set; } = false;

        /// <summary>
        /// GameObject that handle reflection logic for collision
        /// </summary>
        private Collider2D _shieldCollider;

        protected override void Awake()
        {
            base.Awake();

            Shield shield = GetComponentInChildren<Shield>();

            shield.ReflectLayerChange = Data.ReflectLayerChange;

            _shieldCollider = shield.GetComponent<Collider2D>();
        }

        /// <inheritdoc cref="IDamageable.TakeDamage(DamageStats)"/>
        public override float TakeDamage(DamageStats damageStats)
        {
            if (IsReflecting) return default;

            NpcDamageSystem.Instance.ApplyDamage(RuntimeData.BaseStats, damageStats, Data.Resistances, out int damageTaken);
            UIController.Instance.OnTakeDamage(transform.position, damageTaken);
            return damageTaken;
        }

        /// <summary>
        /// Coroutine that enables reflection logic
        /// </summary>
        /// <returns><see cref="IEnumerator"/></returns>
        public IEnumerator StartReflecting()
        {
            IsReflecting = true;

            _shieldCollider.enabled = true;

            float previousSpeed = RuntimeData.BaseStats.Speed;

            RuntimeData.BaseStats.Speed = 0f;

            yield return new WaitForSeconds(Data.ReflectDuration);

            RuntimeData.BaseStats.Speed = previousSpeed;

            _shieldCollider.enabled = false;

            IsReflecting = false;
        }
    }
}