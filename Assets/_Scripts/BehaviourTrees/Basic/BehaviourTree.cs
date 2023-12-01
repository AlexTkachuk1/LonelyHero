using UnityEngine;

namespace Assets._Scripts.BehaviourTrees.Basic
{
    /// <summary>
    /// Behaviour Tree for <see cref="NPC{T}"/> AI
    /// </summary>
    public abstract class BehaviourTree : MonoBehaviour
    {
        /// <summary>
        /// First node of <see cref="BehaviourTree"/>
        /// </summary>
        protected Node _root = null;

        /// <summary>
        /// <inheritdoc cref="SetupTree"/> on Start
        /// </summary>
        protected virtual void Start() => _root = SetupTree();

        /// <summary>
        /// Evaluate <see cref="Node"/> of <see cref="BehaviourTree"/>
        /// </summary>
        protected virtual void FixedUpdate() => _root?.Tick();

        /// <summary>
        /// Create <see cref="BehaviourTree"/>
        /// </summary>
        /// <returns><see cref="_root"/>  <see cref="Node"/></returns>
        protected abstract Node SetupTree();
    }
}