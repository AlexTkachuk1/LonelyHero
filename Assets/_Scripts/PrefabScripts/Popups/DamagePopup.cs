using TMPro;
using UnityEngine;

namespace Assets._Scripts.PrefabScripts.Popups
{
    /// <summary>
    /// Basic damage popup
    /// </summary>
    public class DamagePopup : MonoBehaviour
    {
        /// <summary>
        /// Offset by Y for popup starting position
        /// </summary>
        [SerializeField] private float _offsetY;

        /// <summary>
        /// speed of popup bubbling
        /// </summary>
        [SerializeField] private float _speed;
        [SerializeField] private TextMeshPro _damageText;

        /// <summary>
        /// Time from creation of popups to its destruction.
        /// </summary>
        [SerializeField] private float _popupsLifeTime = 0.5f;

        private void Start()
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + _offsetY);
            Destroy(this.gameObject, _popupsLifeTime);
        }
        private void FixedUpdate() => transform.position += _speed * Time.deltaTime * transform.up;

        /// <summary>
        /// Set text field of a popup
        /// </summary>
        /// <param name="amount">value</param>
        public void SetDamage(float amount) => _damageText.text = amount.ToString();
    }
}