using Assets._Scripts.BehaviourTrees.Basic;
using Assets._Scripts.Models.Enums;
using Assets._Scripts.PrefabScripts.NPCs;

namespace Assets._Scripts
{
    /// <inheritdoc cref="Node"/>
    public class IsReflecting : Node
    {
        /// <inheritdoc cref="ShieldZombie"/>
        private readonly ShieldZombie _shieldZombie;

        public IsReflecting(ShieldZombie shieldZombie)
        {
            _shieldZombie = shieldZombie;
        }

        /// <inheritdoc cref="Node.Tick"/>
        public override NodeStatus Tick() => _shieldZombie.IsReflecting ? NodeStatus.SUCCESS : NodeStatus.FAILURE;
    }
}