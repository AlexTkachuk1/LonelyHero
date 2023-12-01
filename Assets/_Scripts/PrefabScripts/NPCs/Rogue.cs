using System.Collections;
using Assets._Scripts.Models.Base;
using Assets._Scripts.Systems.NPCs;
using UnityEngine;

namespace Assets._Scripts.PrefabScripts.NPCs
{
    /// <inheritdoc cref="NPC{T}"/>
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class Rogue : NPC<RogueData>, IDamageable
    {
        private Rigidbody2D _rigidbody;

        protected override void Awake()
        {
            base.Awake();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        /// <inheritdoc cref="IDamageable.TakeDamage(DamageStats)"/>
        public override float TakeDamage(DamageStats damageStats)
        {
            NpcDamageSystem.Instance.ApplyDamage(RuntimeData.BaseStats, damageStats, Data.Resistances, out int damageTaken);
            UIController.Instance.OnTakeDamage(transform.position, damageTaken);
            return damageTaken;
        }

        /// <summary>
        /// Dash ability for <see cref="Rogue"/>
        /// </summary>
        /// <param name="dashDirection"> Direction for <see cref="Rigidbody2D.velocity"/></param>
        /// <returns></returns>
        public IEnumerator Dash(Vector2 dashDirection)
        {
            _rigidbody.velocity = dashDirection * Data.DashSpeed;

            yield return new WaitForSeconds(Data.DashDuration);

            _rigidbody.velocity = Vector2.zero;
        }
    }
}