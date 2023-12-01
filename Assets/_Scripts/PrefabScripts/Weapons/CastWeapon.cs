using Assets._Scripts.PrefabScripts.Weapons.Base;
using Assets._Scripts.Scriptable;
using Assets._Scripts.Systems;
using UnityEngine;

namespace Assets._Scripts.PrefabScripts.Weapons
{
    /// <inheritdoc cref="Weapon{T}"/>
    public class CastWeapon : Weapon<CastWeaponData>
    {
        protected override void Tick()
        {
            Vector2 playerPosition = PlayerMovementSystem.Instance.PlayerTransform.position;
            float range = _weaponData.Stats.PositionRange;
            transform.position = new Vector2(Random.Range(playerPosition.x - range, playerPosition.x + range),
                                             Random.Range(playerPosition.y - range, playerPosition.y + range));
        }
    }
}