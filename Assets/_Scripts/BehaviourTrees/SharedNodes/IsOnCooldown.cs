using Assets._Scripts.BehaviourTrees.Basic;
using Assets._Scripts.Models.Enums;
using UnityEngine;

namespace Assets._Scripts.BehaviourTrees.SharedNodes
{
    public class IsOnCooldown : Node
    {
        public override NodeStatus Tick()
        {
            object result = GetData(NodeContextItems.NextUsage);

            if (result == null)
                return NodeStatus.SUCCESS;

            return Time.time > (float)result ? NodeStatus.SUCCESS : NodeStatus.FAILURE;
        }
    }
}