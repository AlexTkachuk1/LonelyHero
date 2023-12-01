using Assets._Scripts.Scriptable;
using UnityEngine;

[CreateAssetMenu(fileName = "New magic circl weapon", menuName = "MagicCircl_Weapon")]
public class MagicCircleWeaponData : WeaponData
{
    /// <summary>
    /// The name of the activated weapon animation clip.
    /// </summary>
    [SerializeField] private string _onActivationClipName;

    /// <inheritdoc cref="_onActivationClipName"/>
    public string OnActivationClipName => _onActivationClipName;
}
