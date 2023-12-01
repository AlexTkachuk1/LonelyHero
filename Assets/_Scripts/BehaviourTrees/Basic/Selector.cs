using System.Collections.Generic;
using Assets._Scripts.Models.Enums;

namespace Assets._Scripts.BehaviourTrees.Basic
{
    public sealed class Selector : Node
    {
        public Selector() : base() { }
        public Selector(List<Node> children) : base(children) { }

        public override NodeStatus Tick()
        {
            foreach (Node child in _childrens)
            {
                NodeStatus childStatus = child.Tick();

                if (childStatus != NodeStatus.FAILURE)
                {
                    return childStatus;
                }
            }

            return NodeStatus.FAILURE;
        }
    }
}
