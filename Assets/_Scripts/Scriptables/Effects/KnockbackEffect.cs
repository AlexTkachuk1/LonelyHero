using System.Collections;
using Assets._Scripts.Scriptable.Effects.Base;
using Assets._Scripts.Systems;
using UnityEngine;

/// <inheritdoc cref="WeaponEffect"/>
[CreateAssetMenu(fileName = "New Knockback effect")]
public class KnockbackEffect : WeaponEffect
{
    [SerializeField] private float _thrust;
    [SerializeField] private float _knockTime;

    public override void Perform(GameObject gameObject)
    {
        Rigidbody2D rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        gameObject.GetComponent<MonoBehaviour>().StartCoroutine(Knockback(rigidbody2D, gameObject.transform.position));
    }

    private IEnumerator Knockback(Rigidbody2D rigidbody2D, Vector3 npcPosition)
    {
        rigidbody2D.isKinematic = false;
        Vector2 difference = npcPosition - PlayerMovementSystem.Instance.PlayerTransform.position;
        difference = difference.normalized * _thrust;
        rigidbody2D.AddForce(difference, ForceMode2D.Impulse);

        yield return new WaitForSeconds(_knockTime);

        rigidbody2D.velocity = Vector2.zero;
        rigidbody2D.isKinematic = true;
    }
}