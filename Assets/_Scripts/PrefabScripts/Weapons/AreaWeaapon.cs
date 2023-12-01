using System.Collections;
using Assets._Scripts.PrefabScripts.Weapons.Base;
using Assets._Scripts.Scriptable;
using Assets._Scripts.Systems;
using UnityEngine;

public class AreaWeaapon : Weapon<AreaWeaaponData>
{
    private void Start()
    {
        PlayAnimation(_weaponData.ClipName);
        StartCoroutine(DamageCoroutine());
    }

    /// <summary>
    /// Coroutine that calls the damage method after a certain period of time.
    /// </summary>
    private IEnumerator DamageCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_weaponData.AttackCooldown);
            DealDamage();
        }
    }

    /// <summary>
    /// Deals damage to all enemies in a certain radius.
    /// </summary>
    private void DealDamage()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, _weaponData.AttackRange, _weaponData.EnemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.TryGetComponent(out IDamageable component)) component.TakeDamage(_weaponData.Stats.DamageStats);
        }
    }

    /// <inheritdoc cref="Weapon{T}.Tick"/>
    protected override void Tick()
    {
        Vector2 playerPosition = PlayerMovementSystem.Instance.PlayerTransform.position;
        transform.position = new Vector2(playerPosition.x, playerPosition.y);
    }
}