using System.Collections;
using Assets._Scripts.Models.Base;
using Assets._Scripts.PrefabScripts.Weapons.Base;
using Assets._Scripts.Scriptable.Player;
using Assets._Scripts.Scriptables.Weapons.Base;
using Assets._Scripts.Systems;
using Assets._Scripts.Systems.NPCs;
using UnityEngine;

public class ArmorWeapon : Weapon<ArmorWeaponData>, IDamageable, IReceiveDamage
{
    [SerializeField] private PlayerData _playerData;

    private Collider2D _playerCollider;
    private Collider2D _armorCollider;
    private SpriteRenderer _spriteRenderer;
    private Coroutine _animationCoroutine;

    private void FixedUpdate() => transform.position = PlayerMovementSystem.Instance.PlayerTransform.position;

    protected override void Tick()
    {
    }

    /// <inheritdoc cref="Weapon{T}.CreateWeapon(T)"/>
    public override void CreateWeapon(ArmorWeaponData weaponData)
    {
        _weaponData = weaponData;
        _animator = GetComponent<Animator>();
        _armorCollider = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _playerCollider = PlayerMovementSystem.Instance.PlayerTransform.GetComponent<Collider2D>();
        _animationCoroutine = StartCoroutine(ToggleArmorCoroutine());
    }

    /// <inheritdoc cref="Weapon{T}.ToggleArmorCoroutine()"/>
    protected override IEnumerator ToggleArmorCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_weaponData.Stats.Countdown);

            SetArmorEnable(true);

            PlayAnimation(_weaponData.ClipName);

            yield return new WaitForSeconds(_weaponData.Duration);

            SetArmorEnable(false);

            _weaponData.ResetBlockAmount();
        }
    }

    /// <inheritdoc cref="IDamageable.TakeDamage(DamageStats)"/>
    public float TakeDamage(DamageStats damageStats)
    {
        NpcDamageSystem.Instance.ApplyDamage(_weaponData.ArmourStats, damageStats, _playerData.Resistances, out int damageTaken);
        UIController.Instance.OnTakeDamage(transform.position, damageTaken);

        if (!_weaponData.ArmourStats.IsAlive)
        {
            StopCoroutine(_animationCoroutine);

            SetArmorEnable(false);

            _animationCoroutine = StartCoroutine(ToggleArmorCoroutine());
        }

        return damageTaken;
    }

    /// <summary>
    /// Disables view ( <see cref="_spriteRenderer"/> ) and <see cref="_armorCollider"/> for armor and change state of <see cref="_playerCollider"/>
    /// </summary>
    /// <param name="status">bool flag</param>
    private void SetArmorEnable(bool status)
    {
        _armorCollider.enabled = status;
        _spriteRenderer.enabled = status;
        _playerCollider.enabled = !status;
    }
}