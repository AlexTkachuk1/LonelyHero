using System.Collections.Generic;
using Assets._Scripts.Models.Enums;

namespace Assets._Scripts.BehaviourTrees.Basic
{
    /// <summary>
    /// Basic part of <see cref="BehaviourTree"/>
    /// </summary>
    public abstract class Node
    {
        /// <summary>
        /// Shared Nodes data
        /// </summary>
        private readonly Dictionary<NodeContextItems, object> _dataContext = new();

        /// <summary>
        /// Previous <see cref="Node"/> of <see cref="Sequence"/> or <see cref="Selector"/>
        /// </summary>
        public Node Parent;

        /// <inheritdoc cref="NodeStatus"/>
        protected NodeStatus _status;

        /// <summary>
        /// Other <see cref="Node"/>s of <see cref="Sequence"/> or <see cref="Selector"/> that attached to this <see cref="Node"/>
        /// </summary>
        protected List<Node> _childrens = new();

        public Node() => Parent = null;

        /// <summary>
        /// Constructor that allows to create nested <see cref="BehaviourTree"/> structure
        /// </summary>
        /// <param name="childrens"></param>
        public Node(List<Node> childrens)
        {
            foreach (var children in childrens)
                Attach(children);
        }

        /// <summary>
        /// Link this <see cref="Node"/> as <see cref="Parent"/> and add it to <inheritdoc cref="_childrens"/>
        /// </summary>
        /// <param name="children"></param>
        private void Attach(Node children)
        {
            children.Parent = this;
            _childrens.Add(children);
        }

        /// <summary>
        /// Evaluate logic for this <see cref="Node"/>
        /// </summary>
        /// <returns>execution <see cref="NodeStatus"/></returns>
        public abstract NodeStatus Tick();

        /// <summary>
        /// Add data to shared context
        /// </summary>
        public void SetData(NodeContextItems key, object value)
        {
            _dataContext[key] = value;
        }

        /// <summary>
        /// Get data from shared context
        /// </summary>
        public object GetData(NodeContextItems key)
        {
            object value = null;
            if (_dataContext.TryGetValue(key, out value))
                return value;

            Node node = Parent;
            while (node != null)
            {
                value = node.GetData(key);
                if (value != null)
                    return value;
                node = node.Parent;
            }
            return null;
        }

        /// <summary>
        /// Clear data in shared context
        /// </summary>
        public bool ClearData(NodeContextItems key)
        {
            if (_dataContext.ContainsKey(key))
            {
                _dataContext.Remove(key);
                return true;
            }

            Node node = Parent;
            while (node != null)
            {
                bool cleared = node.ClearData(key);
                if (cleared)
                    return true;
                node = node.Parent;
            }
            return false;
        }
    }
}