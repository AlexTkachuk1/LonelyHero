using Assets._Scripts.PrefabScripts.Weapons.Base;
using Assets._Scripts.Systems;
using UnityEngine;

namespace Assets._Scripts.PrefabScripts.Weapons
{
    /// <inheritdoc cref="Weapon{T}"/>
    public class ProjectileWeapon : Weapon<ProjectileWeaponData>
    {
        /// <summary>
        /// Direction of the projectile;
        /// </summary>
        public Vector3 Direction;

        /// <summary>
        /// RigidBody of the projectile;
        /// </summary>
        private Rigidbody2D _rigidbody2D;

        /// <summary>
        /// Default layer of the projectile;
        /// </summary>
        private int _layer;

        private void Start()
        {
            SetRandomTransform();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _layer = gameObject.layer;
        }

        private void FixedUpdate() => _rigidbody2D.velocity = _weaponData.Speed * Time.deltaTime * Direction.normalized;

        /// <summary>
        /// Sets a random transform on an object.
        /// </summary>
        private void SetRandomTransform()
        {
            Direction = Helpers.GetRandomVector();
            transform.rotation = Helpers.GetAngle(Direction);
        }

        /// <summary>
        /// Sets the position of the object to the start of the cast.
        /// </summary>
        protected override void Tick()
        {
            SetRandomTransform();

            gameObject.layer = _layer;

            Vector2 playerPosition = PlayerMovementSystem.Instance.PlayerTransform.position;
            transform.position = new Vector2(playerPosition.x, playerPosition.y);
        }
    }
}