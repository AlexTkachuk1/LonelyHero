using Assets._Scripts.PrefabScripts.Weapons.Base;
using Assets._Scripts.Systems;
using UnityEngine;

namespace Assets._Scripts.PrefabScripts.Weapons
{
    public class FollowingProjectileWeapon : Weapon<ProjectileWeaponData>
    {
        /// <summary>
        /// Target of the projectile;
        /// </summary>
        private Vector3 _target;

        /// <summary> 
        /// Random float value
        /// </summary>
        public float GetRandomFloat
        {
            get
            {
                int cof = Random.Range(0,2) * 2 - 1;
                return Random.Range(_weaponData.Stats.PositionRange * 0.8f, _weaponData.Stats.PositionRange) * cof;
            }
        }

        private void Start() => SetNewTarget();

        private void FixedUpdate() => FollowTheTarget();

        /// <summary>
        /// Sets new random target,and then sets the angle of the projectile according to the direction of its movement.
        /// </summary>
        private void SetNewTarget()
        {
            Vector3 playerPosition = PlayerMovementSystem.Instance.PlayerTransform.position;

            _target = new Vector3(playerPosition.x + GetRandomFloat, playerPosition.y + GetRandomFloat);

            float angle = (float)(Mathf.Atan2(_target.y - playerPosition.y, _target.x - playerPosition.x) * 180 / Mathf.PI);

            Quaternion rotation = new()
            {
                eulerAngles = new Vector3(0, 0, angle)
            };
            transform.rotation = rotation;
        }

        /// <summary>
        /// Moves the projectile in the direction of the target, if the target is reached, selects a new one.
        /// </summary>
        private void FollowTheTarget()
        {
            if ((transform.position - _target).sqrMagnitude < 1 * 1) SetNewTarget();
            transform.position = Vector3.MoveTowards(transform.position, _target, _weaponData.Speed * Time.deltaTime);
        }

        protected override void Tick()
        {
        }
    }
}