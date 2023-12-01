using Assets._Scripts.Models.Enums;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets._Scripts.Systems
{
    /// <summary>
    /// <see cref="Units.Player.Player"/> movement system.
    /// </summary>
    [RequireComponent(typeof(InputSystem))]
    [RequireComponent(typeof(Animator))]
    public sealed class PlayerMovementSystem : Singleton<PlayerMovementSystem>
    {
        #region Properties
        /// <inheritdoc cref=" _playerTransform"/>
        public Transform PlayerTransform { get => _playerTransform; }

        /// <inheritdoc cref="LevelData"/>
        [SerializeField] private LevelData _level;

        /// <summary>
        /// Player new Unity InputSystem move action
        /// </summary>
        [SerializeField] private InputActionReference _move;

        /// <summary>
        /// Player animator component
        /// </summary>
        [SerializeField] private Animator _animator;

        /// <summary>
        /// Player position relative to weapon spawn position
        /// </summary>
        [SerializeField] private Transform _playerTransform;

        #endregion

        /// <summary>
        /// OnEnable invokes on object creation , adds methods references to InpusActions
        /// </summary>
        private void OnEnable()
        {
            _move.action.performed += PerformMove;
            _move.action.canceled += PerformIdle;
        }

        /// <summary>
        /// OnDisable invokes on object destruction , removes methods references from InpusActions
        /// </summary>
        private void OnDisable()
        {
            _move.action.performed -= PerformMove;
            _move.action.canceled -= PerformIdle;
        }

        /// <summary>
        /// Move player based on <see cref="GetVector"/>
        /// </summary>
        /// <param name="rigidbody2D"></param>
        public void Move(Rigidbody2D rigidbody2D, Transform transform, float playerSpeed)
        {
            Vector2 axis = GetVector();

            rigidbody2D.velocity = playerSpeed * Time.deltaTime * axis;

            Helpers.Rotate(axis.x, transform);

            _playerTransform.position = _level.GetUnitBoundedPosition(_playerTransform.position);
        }

        /// <summary>
        /// Method that subscribes/unsubscribes to move action and provides Move animation
        /// </summary>
        /// <param name="obj"></param>
        private void PerformMove(InputAction.CallbackContext obj) => _animator.SetInteger(nameof(CharacterState), (int)CharacterState.Walk);

        /// <summary>
        /// Method that subscribes/unsubscribes to move action and provides Idle animation
        /// </summary>
        /// <param name="obj"></param>
        private void PerformIdle(InputAction.CallbackContext obj) => _animator.SetInteger(nameof(CharacterState), (int)CharacterState.Idle);

        /// <summary>
        /// Read value from <see cref="_move"/>
        /// </summary>
        /// <returns><see cref="Vector2"/> Vector in which player moves</returns>
        private Vector2 GetVector() => _move.action.ReadValue<Vector2>();
    }
}