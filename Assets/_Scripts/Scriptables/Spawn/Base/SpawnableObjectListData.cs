using Assets._Scripts.Scriptable.Base;
using UnityEngine;

namespace Assets._Scripts.Scriptable.Spawn.Base
{
    /// <summary>
    /// <see cref="ScriptableObject"/> that represents block of enemyes that <see cref="Systems.NPCs.NpcSpawnSystem"/> can spawn
    /// </summary>
    [CreateAssetMenu(fileName = "New Spawnable Object List", menuName = "Spawnable Object List")]
    public sealed class SpawnableObjectList : ScriptableObject
    {
        /// <summary>
        /// Array of <see cref="SpawnableObject"/> s 
        /// </summary>
        [SerializeField] private SpawnableObject[] _spawnableObjects;

        /// <inheritdoc cref="_spawnableObjects"/>
        public SpawnableObject[] SpawnableObjects { get => _spawnableObjects; }
    }
}