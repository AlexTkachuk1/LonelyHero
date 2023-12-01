using Assets._Scripts.Models.Base;
using Assets._Scripts.Scriptable;
using UnityEngine;

/// <inheritdoc cref="WeaponData"/>
[CreateAssetMenu(fileName = "New armor weapon", menuName = "Armor_Weapon")]
public class ArmorWeaponData : WeaponData
{
        /// <summary>
        /// Time which defines armor duration and collider being active
        /// </summary>
        [SerializeField] private float _duration;

        /// <inheritdoc cref="BaseStats"/>
        [SerializeField] private BaseStats _armorStats;

        /// <inheritdoc cref="_duration"/>
        public float Duration { get => _duration; }
        
        /// <inheritdoc cref="_stats"/>
        public BaseStats ArmourStats { get => _armorStats; }

        /// <summary>
        /// Sets armor health back to <see cref="BaseStats.MaxHealth"/>
        /// </summary>
        public void ResetBlockAmount() => _armorStats.Health = _armorStats.MaxHealth;
}