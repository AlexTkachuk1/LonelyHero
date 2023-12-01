using Assets._Scripts.Scriptable.NPCs.Base;
using UnityEngine;

namespace Assets._Scripts.Scriptables.NPCs
{
    /// <inheritdoc cref="NPC{T}"/>
    [CreateAssetMenu(fileName = "New Reflector data")]
    public class ReflectorData : NpcData
    {
        /// <summary>
        /// Change Reflected collider layers to
        /// </summary>
        [SerializeField] private int _reflectLayerChange;

        /// <summary>
        /// Cooldown of reflection
        /// </summary>
        [SerializeField] private float _reflectCooldown;

        /// <summary>
        /// Time of reflection
        /// </summary>
        [SerializeField] private float _reflectionDuration;

        /// <inheritdoc cref="_reflectCooldown"/>
        public float ReflectCooldown => _reflectCooldown;

        /// <inheritdoc cref="_reflectionDuration"/>
        public float ReflectDuration => _reflectionDuration;
        
        /// <inheritdoc cref="_reflectLayerChange"/>
        public int ReflectLayerChange => _reflectLayerChange;
    }
}