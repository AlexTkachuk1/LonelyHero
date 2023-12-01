using Assets._Scripts.Scriptable;
using UnityEngine;

/// <inheritdoc cref="WeaponData"/>
[CreateAssetMenu(fileName = "New projectile weapon", menuName = "Projectile_Weapon")]
public class ProjectileWeaponData : WeaponData
{
    /// <summary>
    /// Projectile speed.
    /// <summary>
    [SerializeField] private float _speed;

    /// <inheritdoc cref="_speed"/>
    public float Speed
    {
        get => _speed;

        set
        {
            if (_speed < 0)
                _speed = 0;

            _speed = value;
        }
    }
}
