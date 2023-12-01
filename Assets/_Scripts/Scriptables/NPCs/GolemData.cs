using Assets._Scripts.Scriptable.NPCs.Base;
using UnityEngine;

[CreateAssetMenu(fileName = "New Golem data")]
public class GolemData : NpcData
{
    /// <summary>
    /// <see cref="Assets._Scripts.PrefabScripts.NPCs.Golem"/>  preparation duration
    /// </summary>
    [SerializeField] private float _preparationDuration;

    /// <summary>
    /// <see cref="Assets._Scripts.PrefabScripts.NPCs.Golem"/>  preparation range
    /// </summary>
    [SerializeField] private float _preparationRange;

    /// <summary>
    /// <see cref="Assets._Scripts.PrefabScripts.NPCs.Golem"/>  minimum dash distance
    /// </summary>
    [SerializeField] private float _minDashRange;

    /// <summary>
    /// <see cref="Assets._Scripts.PrefabScripts.NPCs.Golem"/>  dash distance
    /// </summary>
    [SerializeField] private float _dashRange;

    /// <summary>
    /// <see cref="Assets._Scripts.PrefabScripts.NPCs.Golem"/>  dash speed
    /// </summary>
    [SerializeField] private float _dashSpeed;

    /// <summary>
    /// <see cref="Assets._Scripts.PrefabScripts.NPCs.Golem"/>  target layer
    /// </summary>
    [SerializeField] private LayerMask _targetLayer;

    /// <summary>
    /// <see cref="Assets._Scripts.PrefabScripts.NPCs.Golem"/>  dash duration
    /// </summary>
    [SerializeField] private float _dashDuration;

    /// <inheritdoc cref="_preparationDuration"/>
    public float PreparationDuration { get => _preparationDuration; }

    /// <inheritdoc cref="_preparationRange"/>
    public float PreparationRange { get => _preparationRange; }

    /// <inheritdoc cref="_minDashRange"/>
    public float MinDashRange { get => _minDashRange; }

    /// <inheritdoc cref="_dashRange"/>
    public float DashRange { get => _dashRange; }

    /// <inheritdoc cref="_targetLayer"/>
    public LayerMask TargetLayer { get => _targetLayer; }

    /// <inheritdoc cref="_dashSpeed"/>
    public float DashSpeed { get => _dashSpeed; }

    /// <inheritdoc cref="_dashDuration"/>
    public float DashDuration { get => _dashDuration; }
}
