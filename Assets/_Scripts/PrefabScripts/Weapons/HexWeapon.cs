using Assets._Scripts.PrefabScripts.Weapons.Base;
using Assets._Scripts.Systems;
using UnityEngine;

public class HexWeapon : Weapon<HexWeaponData>
{
    /// <summary>
    /// Direction of the projectile;
    /// </summary>
    private Vector3 _direction;
    private SpriteRenderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        SetRandomTransform();
    }

    private void FixedUpdate() => transform.position += _weaponData.Speed * Time.deltaTime * _direction.normalized;

    /// <summary>
    /// Creates a hex object and places it inside the NPC with which the collision was registered.
    /// </summary>
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageable component))
        {
            Transform npsTansform = collision.GetComponent<Transform>();
            GameObject hexMark = Instantiate(_weaponData.HexMarkPrefab, npsTansform.position, Quaternion.identity, npsTansform);

            Hex hex = hexMark.GetComponent<Hex>();

            hex.SetBaseStats(_weaponData.Stats.DamageStats, _weaponData.Stats.Countdown,
                                  _weaponData.EnemyLayer, _weaponData.Stats.PositionRange);
            hex.StartHexCoroutine();

            SpriteRenderer spriteRenderer = collision.GetComponentInChildren<SpriteRenderer>();
            spriteRenderer.color = _weaponData.HexColor;
            transform.position = Vector3.zero;
            _renderer.enabled = false;
        }
    }

    /// <summary>
    /// Sets a random transform on an object.
    /// </summary>
    private void SetRandomTransform()
    {
        _direction = Helpers.GetRandomVector();
        float angle = (float)(Mathf.Atan2(_direction.y, _direction.x) * 180 / Mathf.PI);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    protected override void Tick()
    {
        _renderer.enabled = true;
        SetRandomTransform();
        Vector2 playerPosition = PlayerMovementSystem.Instance.PlayerTransform.position;
        transform.position = new Vector2(playerPosition.x, playerPosition.y);
    }
}
