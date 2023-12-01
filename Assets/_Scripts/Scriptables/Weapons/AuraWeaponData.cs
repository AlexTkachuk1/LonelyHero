using Assets._Scripts.Scriptable;
using UnityEngine;

[CreateAssetMenu(fileName = "New aura weapon", menuName = "Aura_Weapon")]
public class AuraWeaponData : WeaponData
{
    /// <summary>
    /// Enemy layer.
    /// </summary>
    [SerializeField] private LayerMask _enemyLayer;


    /// <inheritdoc cref="_enemyLayer"/>
    public LayerMask EnemyLayer => _enemyLayer;
}