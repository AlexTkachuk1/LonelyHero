using Assets._Scripts.Scriptable.NPCs.Base;
using UnityEngine;

/// <inheritdoc cref="NPC{T}"/>
[CreateAssetMenu(fileName = "New Rogue data")]
public class RogueData : NpcData
{
    /// <summary>
    /// <see cref="LayerMask"/> of evasion targets <see cref="Assets._Scripts.PrefabScripts.NPCs.Rogue"/>
    /// </summary>
    [SerializeField] private LayerMask _evasionTargets;

    /// <summary>
    /// <see cref="Assets._Scripts.PrefabScripts.NPCs.Rogue"/>  dash speed
    /// </summary>
    [SerializeField] private float _dashSpeed;

    /// <summary>
    /// <see cref="Assets._Scripts.PrefabScripts.NPCs.Rogue"/>  dash radius
    /// </summary>
    [SerializeField] private float _dashRadius;

    /// <summary>
    /// <see cref="Assets._Scripts.PrefabScripts.NPCs.Rogue"/>  dash duration
    /// </summary>
    [SerializeField] private float _dashDuration;

    /// <summary>
    /// <see cref="Assets._Scripts.PrefabScripts.NPCs.Rogue"/>  dash countdown
    /// </summary>
    [SerializeField] private float _dashCooldown;

    /// <inheritdoc cref="_dashSpeed"/>
    public float DashSpeed { get => _dashSpeed; }

    /// <inheritdoc cref="_dashDuration"/>
    public float DashDuration { get => _dashDuration; }

    /// <inheritdoc cref="_dashCooldown"/>
    public float DashCooldown { get => _dashCooldown; }

    /// <inheritdoc cref="_dashRadius"/>
    public float DashRadius { get => _dashRadius; }

    /// <inheritdoc cref="_evasionTargets"/>
    public LayerMask EvasionTargets { get => _evasionTargets; }
}