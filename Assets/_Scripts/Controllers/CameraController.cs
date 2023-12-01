using Assets._Scripts.Systems;
using UnityEngine;

namespace Assets._Scripts.Controllers
{
    /// <summary>
    /// Camera smooth controller script
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        /// <inheritdoc cref="LevelData"/>
        [SerializeField] private LevelData _levelData;

        /// <summary>
        /// parameetr that add smotthness to camera
        /// </summary>
        [SerializeField] private float _smoothSpeed = 0.125f;

        /// <summary>
        /// Offset for camera view
        /// </summary>
        [SerializeField] private Vector3 _offset;

        private void LateUpdate()
        {
            Vector3 desiredPosition = PlayerMovementSystem.Instance.PlayerTransform.position + _offset;
            Vector3 boundedPosition = _levelData.GetCameraBoundedPosition(desiredPosition);

            Vector3 smoothedPosition = Vector3.Lerp(transform.position, boundedPosition, _smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
