using Assets._Scripts.Scriptable;
using UnityEngine;

[CreateAssetMenu(fileName = "New weapon crafting weapon", menuName = "WeaponCrafting_Weapon")]
public class WeaponCraftingWeaponData : WeaponData
{
    /// <summary>
    /// Weapon data which will be instantiated on Tick.
    /// </summary>
    [SerializeField] private WeaponData _trapScripableObject;

    /// <summary>
    /// Ñhild weapon lifetime.
    /// </summary>
    [SerializeField] private float _weaponLiftime;

    /// <inheritdoc cref="_trapScripableObject"/>
    public WeaponData TrapScripableObject => _trapScripableObject;

    /// <inheritdoc cref="_weaponLiftime"/>
    public float WeaponLiftime => _weaponLiftime;
}
