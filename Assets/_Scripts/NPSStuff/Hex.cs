using System.Collections;
using Assets._Scripts.Models.Base;
using UnityEngine;

public class Hex : MonoBehaviour
{
    /// <summary>
    /// Time between hex ticks.
    /// </summary>
    private float _attackCooldown;

    /// <inheritdoc cref="Scriptable.Base.DamageStats"/>
    private DamageStats _damageStats;

    /// <summary>
    /// Object layers affected by the hex effect.
    /// </summary>
    private LayerMask _enemyLayer;

    /// <summary>
    /// Attack radius.
    /// </summary>
    private float _attackRange;

    public void SetBaseStats(DamageStats damageStats, float countdown, LayerMask enemyLayer, float attackRange)
    {
        _damageStats = damageStats;
        _attackCooldown = countdown;
        _enemyLayer = enemyLayer;
        _attackRange = attackRange;
    }

    /// <summary>
    /// Starts hex coroutine.
    /// </summary>
    public void StartHexCoroutine() => StartCoroutine(HexCoroutine());

    /// <summary>
    /// Deals damage to <see cref="IDamageable"/> objects within a certain radius.
    /// </summary>
    private void DealDamage()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, _attackRange, _enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.TryGetComponent(out IDamageable component)) component.TakeDamage(_damageStats);
        }
    }

    private IEnumerator HexCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_attackCooldown);
            DealDamage();
        }
    }
}
