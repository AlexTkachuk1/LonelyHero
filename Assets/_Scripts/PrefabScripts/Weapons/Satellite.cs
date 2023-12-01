using UnityEngine;

public class Satellite : MonoBehaviour
{
    private SatelliteWeapon _satelliteWeapon;
    private void Start() => _satelliteWeapon = GetComponentInParent<SatelliteWeapon>();
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageable component)) component.TakeDamage(_satelliteWeapon.DamageStats);
    }
}
