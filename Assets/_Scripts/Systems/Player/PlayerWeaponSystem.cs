using Assets._Scripts.PrefabScripts.Weapons.Base;
using Assets._Scripts.Scriptable;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Scripts.Systems
{
    /// <summary>
    /// <see cref="Units.Player.Player"/> weapon system.
    /// </summary>
    public class PlayerWeaponSystem : Singleton<PlayerWeaponSystem>
    {
        #region Properties
        /// <summary>
        /// List of availiable weapons
        /// </summary>
        [SerializeField] private List<WeaponData> _weapons;
        #endregion

        /// <summary>
        /// Get <see cref="WeaponData"/> adding it to lis of weapons and spawns weapon according to this Scriptable object data
        /// </summary>
        /// <param name="weaponData"></param>
        public void AddWeapon<T>(T weaponData) where T : WeaponData
        {
            if (weaponData != null && !_weapons.Contains(weaponData))
            {
                _weapons.Add(weaponData);

                for (int i = 0; i < weaponData.Stats.CastQuantity; i++)
                {
                    GameObject weapon = Instantiate(weaponData.Prefab, PlayerMovementSystem.Instance.PlayerTransform.position, Quaternion.identity, transform);
                    weapon.GetComponent<Weapon<T>>().CreateWeapon(weaponData);
                }
            }
        }
    }
}