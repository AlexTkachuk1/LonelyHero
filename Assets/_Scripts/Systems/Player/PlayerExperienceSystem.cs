using Assets._Scripts.Scriptable.Player;
using UnityEngine;

namespace Assets._Scripts.Systems.Player
{
    public sealed class PlayerExperienceSystem : Singleton<PlayerExperienceSystem>
    {
        /// <inheritdoc cref="PlayerData"/>
        [SerializeField] private PlayerData _data;
        public void AddExperience(float experience) => _data.Experience += experience;
    }
}