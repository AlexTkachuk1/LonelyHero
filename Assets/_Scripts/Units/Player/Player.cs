using Assets._Scripts.Models.Base;
using Assets._Scripts.Models.Enums;
using Assets._Scripts.Scriptable;
using Assets._Scripts.Scriptable.Player;
using Assets._Scripts.Systems;
using Assets._Scripts.Systems.Player;
using UnityEngine;

namespace Assets._Scripts.Units.Player
{
    /// <summary>
    /// Player Game Object
    /// </summary>
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class Player : MonoBehaviour, IDamageable
    {
        /// FOR TESTING PURPOSES
        [SerializeField] private ProjectileWeaponData _projectileWeaponData;
        [SerializeField] private AuraWeaponData _auraweaponData;
        [SerializeField] private ArmorWeaponData _armorWeaponData;
        [SerializeField] private CastWeaponData _castWeaponData;
        [SerializeField] private AreaWeaaponData _areaWeaapon;
        [SerializeField] private SatelliteWeaponData _satelliteWeaapon;
        [SerializeField] private WeaponCraftingWeaponData _weaponCraftingWeapon;
        [SerializeField] private HexWeaponData _hexWeapon;
        /// FOR TESTING PURPOSES

        [SerializeField] private PlayerData _data;

        private Animator _animator;
        private Rigidbody2D _rb;
        private Collider2D _collider2D;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _collider2D = GetComponent<Collider2D>();

            _data.BaseStats.Health = _data.BaseStats.MaxHealth;

            PlayerWeaponSystem.Instance.AddWeapon(_hexWeapon);
        }

        private void FixedUpdate()
        {
            if (!_data.BaseStats.IsAlive)
                return;

            PlayerMovementSystem.Instance.Move(_rb, transform, _data.BaseStats.Speed);
        }

        /// <inheritdoc cref="IDamageable.TakeDamage(DamageStats)"/>
        public float TakeDamage(DamageStats damageStats)
        {
            PlayerDamageSystem.Instance.ApplyDamage(_data.BaseStats, damageStats, _data.Resistances, out int damageTaken);

            UIController.Instance.OnTakeDamage(transform.position, damageTaken);
            UIController.Instance.OnPlayerHealthChange();

            if (!_data.BaseStats.IsAlive)
            {
                Destroy(PlayerMovementSystem.Instance);

                _rb.velocity = default;
                _collider2D.enabled = false;

                _animator.SetInteger(nameof(CharacterState), (int)CharacterState.Death);
            }

            return damageTaken;
        }

        public void PerformGameOver() => UIController.Instance.PerformGameOver();
    }
}