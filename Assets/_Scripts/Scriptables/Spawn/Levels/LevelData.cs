using UnityEngine;

/// <summary>
/// Common data related to current level
/// </summary>
[CreateAssetMenu(fileName = "New Level Object", menuName = "Level Object")]
public class LevelData : ScriptableObject
{
    /// <summary>
    /// Minimum camera position
    /// </summary>
    [SerializeField] private Vector3 _minCameraBounds;

    /// <summary>
    /// Maximum camera position
    /// </summary>
    [SerializeField] private Vector3 _maxCameraBounds;

    /// <summary>
    /// Minimum unit position
    /// </summary>
    [SerializeField] private Vector2 _minUnitBounds;

    /// <summary>
    /// Maximum unit position
    /// </summary>
    [SerializeField] private Vector2 _maxUnitBounds;

    /// <inheritdoc cref="_minCameraBounds"/>
    public Vector3 MinCameraBounds { get { return _minCameraBounds; } }

    /// <inheritdoc cref="_maxCameraBounds"/>
    public Vector3 MaxCameraBounds { get { return _maxCameraBounds; } }

    /// <inheritdoc cref="_minUnitBounds"/>
    public Vector2 MinUnitBounds { get { return _minUnitBounds; } }

    /// <inheritdoc cref="_maxUnitBounds"/>
    public Vector2 MaxUnitBounds { get { return _maxUnitBounds; } }


    /// <summary>
    /// Make unit vector be in bounds
    /// </summary>
    /// <param name="position">Unit position</param>
    /// <returns>new bounded <see cref="Vector2"/></returns>
    public Vector2 GetUnitBoundedPosition(Vector2 position)
    {
        return new(Mathf.Clamp(position.x, MinUnitBounds.x, MaxUnitBounds.x),
                        Mathf.Clamp(position.y, MinUnitBounds.y, MaxUnitBounds.y));
    }

    /// <summary>
    /// Make camera vector be in bounds
    /// </summary>
    /// <param name="position">Camera position</param>
    /// <returns>new bounded <see cref="Vector3"/></returns>
    public Vector3 GetCameraBoundedPosition(Vector3 position)
    {
        return new(
                Mathf.Clamp(position.x, MinCameraBounds.x, MaxCameraBounds.x),
                Mathf.Clamp(position.y, MinCameraBounds.y, MaxCameraBounds.y),
                Mathf.Clamp(position.z, MinCameraBounds.z, MaxCameraBounds.z));
    }
}