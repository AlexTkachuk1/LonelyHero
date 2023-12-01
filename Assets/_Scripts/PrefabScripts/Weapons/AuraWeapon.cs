using System.Collections;
using Assets._Scripts.PrefabScripts.Weapons.Base;
using Assets._Scripts.Systems;
using UnityEngine;

namespace Assets._Scripts.PrefabScripts.Weapons
{
    /// <summary>
    /// <see cref="Weapon{T}"/> of some area around player
    /// </summary>
    public class AuraWeapon : Weapon<AuraWeaponData>
    {
        private void Start()
        {
            PlayAnimation(_weaponData.ClipName);
            StartCoroutine(DamageCoroutine());
        }

        private void FixedUpdate() => transform.position = PlayerMovementSystem.Instance.PlayerTransform.position;

        /// <summary>
        /// Coroutine that calls the damage method after a certain period of time.
        /// </summary>
        private IEnumerator DamageCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(_weaponData.Stats.Countdown);
                DealDamage();
            }
        }

        /// <summary>
        /// Deals damage to all enemies in a certain radius.
        /// </summary>
        private void DealDamage()
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, _weaponData.Stats.PositionRange, _weaponData.EnemyLayer);

            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy.TryGetComponent(out IDamageable component)) component.TakeDamage(_weaponData.Stats.DamageStats);
            }
        }

        /// <summary>
        /// Enable <see cref="_canBeDamaged"/> on a tick
        /// </summary>
        protected override void Tick()
        {

        }
    }
}