using System.Collections;
using Assets._Scripts.Models.Base;
using Assets._Scripts.Systems;
using Assets._Scripts.Systems.NPCs;
using UnityEngine;

public class Bowman : NPC<BowmanData>, IDamageable
{
    /// <summary>
    /// Arrow prefab.
    /// </summary>
    [SerializeField] private GameObject _arrowPrefab;

    /// <summary>
    /// Arrow speed.
    /// </summary>
    [SerializeField] private float _arrowSpeed;

    /// <summary>
    /// Time until the arrow object is destroyed since it was created.
    /// </summary>
    [SerializeField] private float _arrowLifeTime = 4f;

    /// <summary>
    /// Default npc speed.
    /// </summary>
    private float _startSpeed;

    /// <summary>
    /// Attack is recharged.
    /// </summary>
    private bool _attackIsRecharged;

    public bool CanAttack { get; set; } = true;


    private void Start()
    {
        _startSpeed = RuntimeData.BaseStats.Speed;
        _attackIsRecharged = false;
    }

    /// <inheritdoc cref="IDamageable.TakeDamage(DamageStats)"/>
    public override float TakeDamage(DamageStats damageStats)
    {
        NpcDamageSystem.Instance.ApplyDamage(RuntimeData.BaseStats, damageStats, Data.Resistances, out int damageTaken);
        UIController.Instance.OnTakeDamage(transform.position, damageTaken);
        return damageTaken;
    }

    /// <summary>
    /// Start attack recharg.
    /// </summary>
    public void StartAttackRecharg()
    {
        if (!_attackIsRecharged)
        {
            StartCoroutine(RechargAttack());
            _attackIsRecharged = true;
        }
    }

    /// <summary>
    /// Recharg attack.
    /// </summary>
    private IEnumerator RechargAttack()
    {
        yield return new WaitForSeconds(Data.ResetAttackCooldown);

        CanAttack = true;
        _attackIsRecharged = false;
    }

    public void Attack()
    {
        GameObject arrow = Instantiate(_arrowPrefab, transform.position, Quaternion.identity, transform);
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        Vector2 targetPosition = (Vector2)PlayerMovementSystem.Instance.PlayerTransform.position;
        Vector2 enemyDirection = targetPosition - (Vector2)transform.position;

        rb.velocity = enemyDirection.normalized * _arrowSpeed;

        float angle = (float)(Mathf.Atan2(enemyDirection.y, enemyDirection.x) * 180 / Mathf.PI);

        arrow.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        Destroy(arrow, _arrowLifeTime);

        CanAttack = false;
        RuntimeData.BaseStats.Speed = _startSpeed;
    }
}