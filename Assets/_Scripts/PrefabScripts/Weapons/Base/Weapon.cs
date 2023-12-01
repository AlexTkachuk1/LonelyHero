using Assets._Scripts.Scriptable;
using Assets._Scripts.Scriptable.Effects.Base;
using System.Collections;
using UnityEngine;

namespace Assets._Scripts.PrefabScripts.Weapons.Base
{
    /// <summary>
    /// Weapon prefab class
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public abstract class Weapon<T> : MonoBehaviour where T : WeaponData
    {
        /// <inheritdoc cref="WeaponData"/>
        protected T _weaponData;

        /// <summary>
        /// Weapon prefab animator
        /// </summary>
        protected Animator _animator;

        /// <summary>
        /// Tries to apply weapon damage and <see cref="WeaponEffect" to <see cref="IDamageable"/> objects
        /// </summary>
        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IDamageable component))
            {
                foreach (WeaponEffect effect in _weaponData.WeaponEffects)
                {
                    effect.Perform(collision.gameObject);
                }

                component.TakeDamage(_weaponData.Stats.DamageStats);
            }
        }

        /// <summary>
        /// Apply scriptable object to initialize weapon
        /// </summary>
        /// <param name="weaponData"></param>
        public virtual void CreateWeapon(T weaponData)
        {
            _animator = GetComponent<Animator>();
            _weaponData = weaponData;
            StartCoroutine(ToggleArmorCoroutine());
        }

        /// <summary>
        /// Coroutine to play animation with cooldown
        /// </summary>
        /// <returns><see cref="IEnumerator"/></returns>
        protected virtual IEnumerator ToggleArmorCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(_weaponData.Stats.Countdown);
                PlayAnimation(_weaponData.ClipName);
                Tick();
            }
        }

        protected virtual void PlayAnimation(string clipName) => _animator.Play(clipName);

        /// <summary>
        /// Apply some actions on every <see cref="WeaponData.Stats.Countdown"/>
        /// </summary>
        protected abstract void Tick();
    }
}