using Assets._Scripts.PrefabScripts.Weapons.Base;
using Assets._Scripts.Systems;
using UnityEngine;

public class WeaponCraftingWeapon : Weapon<WeaponCraftingWeaponData>
{
    protected override void Tick()
    {
        GameObject weapon = Instantiate(
            _weaponData.TrapScripableObject.Prefab, PlayerMovementSystem.Instance.PlayerTransform.position, Quaternion.identity, transform);
        weapon.GetComponent<MagicCircleWeapon>().CreateWeapon(_weaponData.TrapScripableObject, _weaponData.WeaponLiftime);
    }
}