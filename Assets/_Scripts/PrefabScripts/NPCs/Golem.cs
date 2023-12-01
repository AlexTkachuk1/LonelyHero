using System.Collections;
using Assets._Scripts.Models.Base;
using Assets._Scripts.Systems;
using Assets._Scripts.Systems.NPCs;
using UnityEngine;

public class Golem : NPC<GolemData>, IDamageable
{
    /// <summary>
    /// Default npc speed.
    /// </summary>
    private float _initialSpeed;

    /// <summary>
    /// Target position.
    /// </summary>
    private Vector2 _targetPosition;

    private Rigidbody2D _rigidbody;

    public bool PreparationComplete { get; set; }
    public bool MakesDash { get; set; } = false;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        PreparationComplete = false;
    }

    /// <inheritdoc cref="IDamageable.TakeDamage(DamageStats)"/>
    public override float TakeDamage(DamageStats damageStats)
    {
        NpcDamageSystem.Instance.ApplyDamage(RuntimeData.BaseStats, damageStats, Data.Resistances, out int damageTaken);
        UIController.Instance.OnTakeDamage(transform.position, damageTaken);
        return damageTaken;
    }

    /// <summary>
    /// The method launches the coroutine of the state of preparing the npc for a dash.
    /// </summary>
    public void StartDashPreparation()
    {
        _targetPosition = PlayerMovementSystem.Instance.PlayerTransform.position;

        PreparationComplete = true;
        StartCoroutine(DashPreparation());
    }

    /// <summary>
    /// Sets movement speed to 0 while preparing to dash.
    /// </summary>
    private IEnumerator DashPreparation()
    {
        _initialSpeed = RuntimeData.BaseStats.Speed;
        RuntimeData.BaseStats.Speed = 0;

        yield return new WaitForSeconds(Data.PreparationDuration);

        MakesDash = true;
    }

    /// <summary>
    /// Dashes towards the target.
    /// </summary>
    public IEnumerator Dash()
    {
        Vector2 dashDirection = _targetPosition - (Vector2)transform.position;
        _rigidbody.velocity = dashDirection.normalized * Data.DashSpeed;

        yield return new WaitForSeconds(Data.DashDuration);

        _rigidbody.velocity = Vector2.zero;
        MakesDash = false;
        RuntimeData.BaseStats.Speed = _initialSpeed;
        _targetPosition = Vector2.zero;
    }
}