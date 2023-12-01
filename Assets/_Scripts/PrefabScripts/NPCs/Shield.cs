using Assets._Scripts.PrefabScripts.Weapons;
using UnityEngine;

public class Shield : MonoBehaviour
{
    /// <summary>
    /// Layer that player weapon becomes after block
    /// </summary>
    public int ReflectLayerChange { get; set; }

    /// <summary>
    /// Tries to apply ShieldZombie damage to <see cref="IDamageable"/> objects
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out ProjectileWeapon weapon))
        {
            Vector2 contactPosition = collider.ClosestPoint(collider.transform.position);

            Rigidbody2D projectileRigidBody = collider.gameObject.GetComponent<Rigidbody2D>();

            // Normalize the vector
            contactPosition.Normalize();

            // Get the normal vector (perpendicular)
            Vector2 inNormal = new(-contactPosition.y, contactPosition.x);
            Vector2 inDirection = projectileRigidBody.velocity;

            collider.gameObject.layer = ReflectLayerChange;
            projectileRigidBody.velocity = Vector2.zero;

            weapon.Direction = Vector2.Reflect(inDirection, inNormal);
            weapon.transform.rotation = Helpers.GetAngle(weapon.Direction);
        }
    }
}