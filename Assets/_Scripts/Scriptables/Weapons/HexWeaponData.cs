using Assets._Scripts.Scriptable;
using UnityEngine;

/// <inheritdoc cref="WeaponData"/>
[CreateAssetMenu(fileName = "New hex weapon", menuName = "Hex_Weapon")]
public class HexWeaponData : WeaponData
{
    /// <summary>
    /// Projectile speed.
    /// <summary>
    [SerializeField] private float _speed;

    /// <summary>
    /// Hex prefab;
    /// </summary>
    [SerializeField] private GameObject _hexMarkPrefab;

    /// <summary>
    /// Object layers affected by the hex effect.
    /// </summary>
    [SerializeField] private LayerMask _enemyLayer;

    /// <summary>
    ///  Effect color
    /// </summary>
    [SerializeField] private Color _hexColor;

    /// <inheritdoc cref="_hexColor"/>
    public Color HexColor => _hexColor;

    /// <inheritdoc cref="_hexMarkPrefab"/>
    public GameObject HexMarkPrefab => _hexMarkPrefab;

    /// <inheritdoc cref="_enemyLayer"/>
    public LayerMask EnemyLayer => _enemyLayer;

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
