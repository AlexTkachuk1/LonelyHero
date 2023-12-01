using Assets._Scripts.PrefabScripts.Popups;
using UnityEngine;

namespace Assets._Scripts.Systems
{
    /// <summary>
    /// Basic system for managing <see cref="DamagePopup"/>
    /// </summary>
    public class PopupSystem : Singleton<PopupSystem>
    {
        /// <inheritdoc cref="DamagePopup"/>
        [SerializeField] private GameObject _damagePopup;

        private void Start() => UIController.Instance.TakeDamage += OnDamageTaken;

        /// <summary>
        /// Add <see cref="DamagePopup"/> with some value
        /// </summary>
        /// <param name="position">popup position</param>
        /// <param name="damage">value of a <see cref="DamagePopup"/></param>
        public void OnDamageTaken(Vector2 position, int damage)
        {
            GameObject popup = Instantiate(_damagePopup, position, Quaternion.identity);
            popup.GetComponent<DamagePopup>().SetDamage(damage);
        }
    }
}