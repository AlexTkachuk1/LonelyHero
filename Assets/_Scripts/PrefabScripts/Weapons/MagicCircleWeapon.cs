using Assets._Scripts.Scriptable;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MagicCircleWeapon : MonoBehaviour
{
    /// <summary>
    /// Weapon animator.
    /// </summary>
    private Animator _animator;

    /// <summary>
    /// Weapon data.
    /// </summary>
    private MagicCircleWeaponData _weaponData;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.Play(_weaponData.ClipName);
    }

    public void CreateWeapon(WeaponData weaponData, float lifetime)
    {
        _weaponData = weaponData as MagicCircleWeaponData;
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageable component))
        {
            component.TakeDamage(_weaponData.Stats.DamageStats);
            _animator.Play(_weaponData.OnActivationClipName);
            Destroy(gameObject, 1f);
        }
    }
}