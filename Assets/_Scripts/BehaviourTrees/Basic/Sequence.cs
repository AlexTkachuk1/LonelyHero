using System.Collections.Generic;
using Assets._Scripts.Models.Enums;

namespace Assets._Scripts.BehaviourTrees.Basic
{
    public class Sequence : Node
    {
        public Sequence() : base() { }
        public Sequence(List<Node> children) : base(children) { }

        public override NodeStatus Tick()
        {
            foreach (Node child in _childrens)
            {
                NodeStatus childStatus = child.Tick();

                if (childStatus != NodeStatus.SUCCESS)
                {
                    return childStatus;
                }
            }

            return NodeStatus.SUCCESS;
        }
    }
}
