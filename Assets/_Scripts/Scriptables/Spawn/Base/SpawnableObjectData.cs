using UnityEngine;

namespace Assets._Scripts.Scriptable.Base
{
    /// <summary>
    /// <see cref="ScriptableObject"/> that represents data that needs <see cref="Systems.NPCs.NpcSpawnSystem"/> to spawn <see cref="NPC{T}"/>s
    /// </summary>
    [CreateAssetMenu(fileName = "New Spawnable Object", menuName = "Spawnable Object")]
    public sealed class SpawnableObject : ScriptableObject
    {
        /// <summary>
        /// <see cref="NPC{T}"/> prefab
        /// </summary>
        [SerializeField] private GameObject _prefab;

        /// <summary>
        /// Max objects to spawn
        /// </summary>
        [SerializeField] private int _maxSpawnCount;

        /// <summary>
        /// Min <see cref="NPC{T}"/>s spawn duration
        /// </summary>
        [SerializeField] private int _spawnCountMin;

        /// <summary>
        /// Max <see cref="NPC{T}"/>s spawn duration
        /// </summary>
        [SerializeField] private int _spawnCountMax;

        /// <summary>
        /// Min Time between spawn <see cref="NPC{T}"s/>
        /// </summary>
        [SerializeField] private float _spawnDelayMin;

        /// <summary>
        /// Max Time between spawn <see cref="NPC{T}"s/>
        /// </summary>
        [SerializeField] private float _spawnDelayMax;

        /// <inheritdoc cref="_prefab"/>
        public GameObject Prefab { get => _prefab; }

        /// <inheritdoc cref="_spawnCountMax"/>
        public int SpawnCountMax { get => _spawnCountMax; }

        /// <inheritdoc cref="_spawnable"/>
        public int SpawnCountMin { get => _spawnCountMin; }

        /// <inheritdoc cref="_spawnDelayMax"/>
        public float SpawnDelayMax { get => _spawnDelayMax; }

        /// <inheritdoc cref="_spawnDelayMin"/>
        public float SpawnDelayMin { get => _spawnDelayMin; }

        /// <inheritdoc cref="MaxSpawnCount"/>
        public int MaxSpawnCount { get => _maxSpawnCount; }
    }
}