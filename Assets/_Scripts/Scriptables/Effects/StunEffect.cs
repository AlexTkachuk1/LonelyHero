using System.Collections;
using Assets._Scripts.Scriptable.Effects.Base;
using UnityEngine;

/// <inheritdoc cref="WeaponEffect"/>
[CreateAssetMenu(fileName = "New stun effect")]
public sealed class StunEffect : WeaponEffect
{
    /// <summary>
    ///  Duration of effect in seconds
    /// </summary>
    [SerializeField] private int _stunDuration;

    /// <summary>
    ///  Effect color
    /// </summary>
    [SerializeField] private Color _stunColor;

    /// <inheritdoc cref="_stunDuration"/>
    public int StunDuration => _stunDuration;

    /// <inheritdoc cref="_stunColor"/>
    public Color StunColor => _stunColor;


    /// <inheritdoc cref="WeaponEffect.Perform(MonoBehaviour)"/>
    public override void Perform(GameObject gameObject)
    {
        gameObject.GetComponent<MonoBehaviour>().StartCoroutine(Tick(gameObject));
    }

    /// <summary>
    ///  Stun effect coroutine which waits for end of effect
    /// </summary>
    private IEnumerator Tick(GameObject gameObject)
    {
        IBase npc = gameObject.GetComponent<IBase>();
        Animator animator = gameObject.GetComponent<Animator>();
        SpriteRenderer spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();

        float pausedSpeed = npc.RuntimeData.BaseStats.Speed;
        Color pausedColor = spriteRenderer.color;

        npc.RuntimeData.BaseStats.Speed = default;

        animator.enabled = false;
        spriteRenderer.color = StunColor;


        yield return new WaitForSeconds(StunDuration);

        animator.enabled = true;
        spriteRenderer.color = pausedColor;

        npc.RuntimeData.BaseStats.Speed = pausedSpeed;
    }
}