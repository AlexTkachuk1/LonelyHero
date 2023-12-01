using Assets._Scripts.Models.Base;

/// <summary>
///  Contract to <see cref="NPC"/> that can be damaged
/// </summary>
public interface IDamageable
{
    /// <summary>
    /// Applies <see cref="WeaponStats"/> to <see cref="NPC"/>
    /// </summary>
    /// <param name="damageStats"></param>
    float TakeDamage(DamageStats damageStats);
}