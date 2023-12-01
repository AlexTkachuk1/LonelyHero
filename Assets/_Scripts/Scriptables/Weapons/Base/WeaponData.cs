using System.Collections.Generic;
using Assets._Scripts.Models.Base;
using Assets._Scripts.Scriptable.Effects.Base;
using UnityEngine;

namespace Assets._Scripts.Scriptable
{
    /// <summary>
    /// Scriptable object for <see cref="PrefabScripts.Weapons.CastWeapon"/>
    /// </summary>
    public abstract class WeaponData : ScriptableObject
    {
        /// <summary>
        /// <see cref="Weapon"/> prefab
        /// </summary>
        [SerializeField] private GameObject _prefab;

        /// <inheritdoc cref="WeaponStats"/>
        [SerializeField] private WeaponStats _stats;

        /// <inheritdoc cref="WeaponEffect"/>
        [SerializeField] private WeaponEffect[] _weaponEffects;

        /// <summary>
        /// Clip name which played in weapon animator
        /// </summary>
        [SerializeField] private string _clipName;

        /// <inheritdoc cref="_clipName"/>
        public string ClipName { get => _clipName; }

        /// <inheritdoc cref="WeaponStats"/>
        public WeaponStats Stats { get => _stats; }

        /// <inheritdoc cref="_prefab"/>
        public GameObject Prefab { get => _prefab; }

        /// <inheritdoc cref="_weaponEffects"/>
        public IEnumerable<WeaponEffect> WeaponEffects { get => _weaponEffects; }
    }
}