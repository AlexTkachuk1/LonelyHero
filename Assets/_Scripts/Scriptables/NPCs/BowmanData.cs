using Assets._Scripts.Scriptable.NPCs.Base;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bowman data")]
public class BowmanData : NpcData
{
    /// <summary>
    /// <see cref="Assets._Scripts.PrefabScripts.NPCs.Golem"/>  
    /// Preparation duration.
    /// </summary>
    [SerializeField] private float _resetAttackCooldown;

    /// <summary>
    /// <see cref="Assets._Scripts.PrefabScripts.NPCs.Golem"/>  
    /// Dash speed.
    /// </summary>
    [SerializeField] private float _arrowSpeed;

    /// <summary>
    /// <see cref="Assets._Scripts.PrefabScripts.NPCs.Golem"/>  
    /// Can attack.
    /// </summary>
    public bool CanAttack { get; set; }

    /// <inheritdoc cref="_resetAttackCooldown"/>
    public float ResetAttackCooldown { get => _resetAttackCooldown; }

    /// <inheritdoc cref="_arrowSpeed"/>
    public float ArrowSpeed { get => _arrowSpeed; }
}
