using Assets._Scripts.Scriptable.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.Utilities
{
    public sealed class HealthBar : MonoBehaviour
    {
        [SerializeField] private PlayerData _playerData;

        /// <summary>
        /// HealthBar image for <see cref="Units.Player.Player"/> health
        /// </summary>
        [SerializeField] private Image _healthBar;

        private void Start()
        {
            UpdateBarFill();
            UIController.Instance.PlayerHealthChange += UpdateBarFill;
        }

        private void UpdateBarFill() => _healthBar.fillAmount = _playerData.BaseStats.Health / _playerData.BaseStats.MaxHealth;
    }
}