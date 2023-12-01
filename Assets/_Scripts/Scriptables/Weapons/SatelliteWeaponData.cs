using Assets._Scripts.Scriptable;
using UnityEngine;

/// <inheritdoc cref="WeaponData"/>
[CreateAssetMenu(fileName = "New satellite weapon", menuName = "Satellite_Weapon")]
public class SatelliteWeaponData : WeaponData
{
    /// <summary>
    /// Projectile clip name.
    /// <summary>
    [SerializeField] private string _satelliteClipName;

    /// <summary>
    /// The number of active satellites.
    /// </summary>
    [SerializeField] private int _numberOfActiveSatellites;

    /// <summary>
    /// Projectile speed.
    /// </summary>
    [SerializeField] private float _speed;

    /// <summary>
    /// Max satellite count in satellite weapon.
    /// </summary>
    private readonly int _maxSatelliteCount = 4;

    /// <inheritdoc cref="_numberOfActiveSatellites"/>
    public int NumberOfActiveSatellites
    {
        get
        {
            if (_numberOfActiveSatellites <= _maxSatelliteCount)
                return _numberOfActiveSatellites;
            return _numberOfActiveSatellites;
        }
    }

    /// <inheritdoc cref="_satelliteClipName"/>
    public string SatelliteClipName
    {
        get => _satelliteClipName;
    }

    /// <inheritdoc cref="_speed"/>
    public float Speed
    {
        get => _speed;
    }
}
