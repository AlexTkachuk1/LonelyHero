using UnityEngine;

namespace Assets._Scripts.Scriptable.Effects.Base
{
    /// <summary>
    /// Scriptable object for different effects applied to <see cref="PrefabScripts.Weapons.Base.Weapon{T}"/>
    /// </summary>
    public abstract class WeaponEffect : ScriptableObject
    {
        /// <summary>
        /// Execute effect action on <see cref="NPC" /> collided with <see cref="PrefabScripts.Weapons.Base.Weapon{T}"/>
        /// </summary>
        /// <param name="gameObject"> <see cref="NPC"/> collided with <see cref="PrefabScripts.Weapons.Base.Weapon{T}"/></param>
        public abstract void Perform(GameObject gameObject);
    }
}