using System.Collections;
using Assets._Scripts.Scriptable.Base;
using Assets._Scripts.Scriptable.Spawn.Base;
using UnityEngine;

namespace Assets._Scripts.Systems.NPCs
{
    /// <summary>
    /// Spawn system that uses <see cref="SpawnableObjectList"/> to spawn range of enemies
    /// </summary>
    public class NpcSpawnSystem : Singleton<NpcSpawnSystem>
    {
        #region Properties
        /// <inheritdoc cref="SpawnableObjectList"/>
        [SerializeField] private SpawnableObjectList _spawnableObjectList;

        /// <summary>
        /// ObstacleLayer that blocks spawn of <see cref="NPC{T}"/>
        /// </summary>
        [SerializeField] private LayerMask _obstacleLayer;


        /// <summary>
        /// Deviation for enemies spawn distribution
        /// </summary>
        [SerializeField] private float _standardDeviation;

        /// <inheritdoc cref="LevelData"/>
        [SerializeField] private LevelData _level;

        /// <summary>
        /// Camera position
        /// </summary>
        private Transform _cameraTransform;

        /// <inheritdoc cref="_spawnCoroutine"/>
        private Coroutine _spawnCoroutine;

        /// <summary>
        /// Spawned objects counter
        /// </summary>
        private int _objectsSpawned = 0;

        private float _camHeight = 0;

        private float _camWidth = 0;
        #endregion

        private void Start()
        {
            _cameraTransform = Camera.main.transform;

            // Get the camera size and aspect ratio
            _camHeight = 2f * Camera.main.orthographicSize + 2;
            _camWidth = _camHeight * Camera.main.aspect + 2;

            _spawnCoroutine = StartCoroutine(SpawnCoroutine());
        }

        private void OnDisable()
        {
            // Stop the spawn coroutine when the object is disabled
            if (_spawnCoroutine != null)
            {
                StopCoroutine(_spawnCoroutine);
            }
        }

        /// <summary>
        /// Coroutine to spawn <see cref="NPC"/>
        /// </summary>
        /// <returns><see cref="IEnumerator"/></returns>
        private IEnumerator SpawnCoroutine()
        {
            while (true)
            {
                // Select a random spawnable object
                SpawnableObject spawnableObject = _spawnableObjectList.SpawnableObjects[Random.Range(default, _spawnableObjectList.SpawnableObjects.Length)];

                if (_objectsSpawned < spawnableObject.MaxSpawnCount)
                {
                    var spawnCount = Random.Range(spawnableObject.SpawnCountMin, spawnableObject.SpawnCountMax);
                    for (int i = 0; i < spawnCount; i++)
                    {
                        // Instantiate a new object at a random position within the specified spawn area
                        Vector2 spawnPosition = GetRandomSpawnPosition();

                        bool spawned = InstantiateNotObstructed(spawnPosition, spawnableObject.Prefab);

                        // Increment the number of objects spawned
                        if (spawned)
                            _objectsSpawned++;
                    }
                }

                // Wait for the specified spawn delay
                yield return new WaitForSeconds(Random.Range(spawnableObject.SpawnDelayMin, spawnableObject.SpawnDelayMax));
            }
        }

        /// <summary>
        /// Recursion function to check if spawn isn't blocked and spawn <see cref="NPC"/>
        /// </summary>
        /// <param name="spawnPosition"><see cref="Vector2"/> that represents position to spawn</param>
        /// <param name="prefab"><inheritdoc cref="SpawnableObject.Prefab"/></param>
        /// <returns><see cref="bool"/> value that represents is object spawned or not</returns>
        private bool InstantiateNotObstructed(Vector2 spawnPosition, GameObject prefab)
        {
            var obstructed = IsObstructed(spawnPosition);
            if (obstructed)
            {
                InstantiateNotObstructed(GetRandomSpawnPosition(), prefab);
            }
            else
            {
                Instantiate(prefab, spawnPosition, Quaternion.identity);
            }

            return true;
        }

        /// <summary>
        /// Get random spawn position based on camera position and <inheritdoc cref="_standardDeviation"/>
        /// </summary>
        /// <returns>randon <see cref="Vector2"/> spawn position</returns>
        private Vector2 GetRandomSpawnPosition()
        {
            Vector2 camPos = _cameraTransform.position;

            // Calculate the minimum and maximum x and y coordinates for enemy spawning
            float minX = camPos.x - (_camWidth / 2);
            float maxX = camPos.x + (_camWidth / 2);
            float minY = camPos.y - (_camHeight / 2);
            float maxY = camPos.y + (_camHeight / 2);

            int side = Random.Range(0, 4);

            float meanX = 0f;
            float meanY = 0f;

            switch (side)
            {
                case 0: // Left side
                    meanX = minX;
                    meanY = Random.Range(minY, maxY);
                    break;

                case 1: // Right side
                    meanX = maxX;
                    meanY = Random.Range(minY, maxY);
                    break;

                case 2: // Top side
                    meanX = Random.Range(minX, maxX);
                    meanY = maxY;
                    break;

                case 3: // Bottom side
                    meanX = Random.Range(minX, maxX);
                    meanY = minY;
                    break;
            }


            Vector2 spawnPosition = _level.GetUnitBoundedPosition(new(
                Mathf.Clamp(Random.Range(minX, maxX), NormalDistribution(meanX), NormalDistribution(meanX)),
                Mathf.Clamp(Random.Range(minY, maxY), NormalDistribution(meanY), NormalDistribution(meanY))));

            return spawnPosition;
        }

        /// <summary>
        /// Apply normal distribution to mean
        /// </summary>
        /// <param name="mean">mean to what distribution is applied</param>
        /// <returns><see cref="NormalDistribution(float)"/></returns>
        private float NormalDistribution(float mean)
        {
            float u1 = 1f - Random.value; // uniform(0,1] random number
            float u2 = 1f - Random.value;

            float randStdNormal = Mathf.Sqrt(-2f * Mathf.Log(u1)) * Mathf.Sin(2f * Mathf.PI * u2); // random normal(0,1)
            float randNormal = mean + _standardDeviation * randStdNormal; // random normal(mean, standard deviation)
            return randNormal;
        }

        /// <summary>
        /// Check for obstruction
        /// </summary>
        /// <param name="position"><see cref="NPC"/> position</param>
        /// <returns><see cref="bool"/> value that represents is position obstructed or not</returns>
        private bool IsObstructed(Vector2 position)
        {
            RaycastHit2D hit = Physics2D.Raycast(position, Vector2.zero, 0f, _obstacleLayer);
            return hit.collider != null;
        }
    }
}