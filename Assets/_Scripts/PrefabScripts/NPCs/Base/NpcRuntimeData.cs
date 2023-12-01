using Assets._Scripts.Models.Base;
using Assets._Scripts.Scriptable.NPCs.Base;

namespace Assets._Scripts.PrefabScripts.NPCs.Base
{
    /// <summary>
    /// Class that holds runtime data that changed via <see cref="NPC"/> prefab lifecycle 
    /// </summary>
    public class NpcRuntimeData
    {
        public NpcRuntimeData(NpcData data) => BaseStats = new BaseStats(data.BaseStats);

        /// <inheritdoc cref="Models.Base.BaseStats"/>
        public BaseStats BaseStats { get; set; }
    }
}