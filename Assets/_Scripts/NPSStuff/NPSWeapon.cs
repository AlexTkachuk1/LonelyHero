using Assets._Scripts.Models.Base;
using UnityEngine;

public class NPSWeapon : MonoBehaviour
{
    /// <summary>
    /// <see cref="DamageStats"/>
    /// </summary>
    private DamageStats _damageStats;

    private void Awake() => _damageStats = GetComponentInParent<Bowman>().Data.DamageStats;

    /// <summary>
    /// Tries to apply npc damage to <see cref="IDamageable"/> objects
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageable component)) component.TakeDamage(_damageStats);
    }
}
