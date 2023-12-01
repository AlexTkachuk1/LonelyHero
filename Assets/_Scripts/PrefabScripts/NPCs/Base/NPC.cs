using Assets._Scripts.Models.Base;
using Assets._Scripts.PrefabScripts.NPCs.Base;
using Assets._Scripts.Scriptable.NPCs.Base;
using Assets._Scripts.Scriptables.Weapons.Base;
using Assets._Scripts.Systems;
using UnityEngine;

/// <summary>
/// NPC prefab class
/// </summary>
[RequireComponent(typeof(Animator))]
public abstract class NPC<T> : MonoBehaviour, IBase where T : NpcData
{
    #region Properties

    /// <inheritdoc cref="NpcData"/>
    public T Data;

    /// <inheritdoc cref="NpcRuntimeData"/>
    protected NpcRuntimeData _runtimeData;

    /// <inheritdoc cref="NpcRuntimeData"/>
    public NpcRuntimeData RuntimeData { get => _runtimeData; }

    /// <summary>
    /// <see cref="NPC{T}"/> sprite.
    /// </summary>
    private SpriteRenderer _sprite;

    #endregion

    private void FixedUpdate()
    {
        _sprite.sortingLayerName = PlayerMovementSystem.Instance.PlayerTransform.position.y > transform.position.y
            ? "EnemiesInFrontOfThePlayer"
            : "EnemiesBehindThePlayer";
    }

    /// <summary>
    /// Tries to apply <see cref="NPC{T}"/> damage to <see cref="IDamageable"/> objects
    /// </summary>
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageable component)) component.TakeDamage(Data.DamageStats);
        if (collision.TryGetComponent(out IReceiveDamage _)) TakeDamage(Data.DamageStats);
    }

    /// <summary>
    /// Create new instance to provide runtime change of data from scriptable object for each <see cref="NPC{T}"/> instance
    /// </summary>
    protected virtual void Awake()
    {
        _sprite = GetComponentInChildren<SpriteRenderer>();
        _runtimeData = new NpcRuntimeData(Data);
    }

    /// <summary>
    /// Destroy prefab within specified time ( in seconds )
    /// </summary>
    /// <param name="time">Time in seconds</param>
    protected virtual void DestroyPrefab(float time = 0f) => Destroy(gameObject, time);

    /// <inheritdoc cref="IDamageable.TakeDamage(DamageStats)"/>
    public abstract float TakeDamage(DamageStats damageStats);
}