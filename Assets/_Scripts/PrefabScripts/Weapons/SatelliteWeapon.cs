using Assets._Scripts.Models.Base;
using Assets._Scripts.PrefabScripts.Weapons.Base;
using Assets._Scripts.Systems;
using UnityEngine;

public class SatelliteWeapon : Weapon<SatelliteWeaponData>
{
    /// <summary>
    /// Children transforms in satellite objects.
    /// </summary>
    private readonly Transform[] _satelliteChildrens = new Transform[4];

    /// <summary>
    /// Children transforms array.
    /// </summary>
    private Transform[] _childrens;

    /// <summary>
    /// Structure that represent damage stats.
    /// </summary>
    private DamageStats _damageStats;

    /// <inheritdoc cref="_damageStats"/>
    public DamageStats DamageStats
    {
        get
        {
            return _damageStats;
        }
    }

    private void Start()
    {
        _damageStats = _weaponData.Stats.DamageStats;
        _childrens = GetComponentsInChildren<Transform>();

        int cildrenIndex = 0;
        for (int i = 0; i < _childrens.Length; i++)
        {
            if (_childrens[i] != transform)
            {
                _satelliteChildrens[cildrenIndex] = _childrens[i];
                cildrenIndex++;
            }
        }
    }

    private void FixedUpdate() => transform.position = PlayerMovementSystem.Instance.PlayerTransform.position;

    protected override void Tick()
    {
        _animator.SetFloat("animSpeed", _weaponData.Speed);

        for (int i = 0; i < _weaponData.NumberOfActiveSatellites; i++)
        {
            Animator animator = _satelliteChildrens[i].GetComponent<Animator>();
            animator.Play(_weaponData.SatelliteClipName);
        }
    }
}