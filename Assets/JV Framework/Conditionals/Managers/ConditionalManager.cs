using System.Collections.Generic;
using UnityEngine;

namespace JVFramework.Collection
{
    [System.Serializable]
    public class ConditionalManager
    {
        public bool FindInChildren;
        public bool CurrentState { get; private set; }

        [Tooltip("Listeners that should be subscribed but would not be found by the search through Children")]
        [SerializeField] private List<AConditionalListener> _ExternalListeners;

        /// <summary>
        /// Constructor
        /// </summary>
        public ConditionalManager() => FindInChildren = false;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pFindInChildren">Should the Object search through all hierarchical objects when changing state</param>
        public ConditionalManager(bool pFindInChildren) => FindInChildren = pFindInChildren;

        /// <summary>
        /// Sets all subscribed Conditionals to the given state
        /// </summary>
        /// <param name="pSource">Source of the Gameobject to look for IConditionals from</param>
        /// <param name="pState">State of the Conditional to be set to</param>
        public void SetState(GameObject pSource, bool pState)
        {
            CurrentState = pState;

            AConditionalListener[] conditionals = GetConditionals(pSource, FindInChildren);

            foreach(AConditionalListener e in conditionals)
            {
                if (e == null) continue;
                if (pState) e.OnTrue();
                else e.OnFalse();
            }
        }

        private AConditionalListener[] GetConditionals(GameObject pSource, bool pContinueInChildren)
        {
            List<AConditionalListener> conditionals = new List<AConditionalListener>(pSource.GetComponents<AConditionalListener>());
            if((_ExternalListeners?.Count ?? 0) > 0)
                conditionals.AddRange(_ExternalListeners);
            Transform[] children = pSource.GetComponentsInChildren<Transform>(true);
            foreach (Transform t in children) if (t.GetComponents<AConditionalListener>() != null) conditionals.AddRange(t.GetComponents<AConditionalListener>());
            foreach (AConditionalListener e in conditionals) e.SetManager(this);

            return conditionals.ToArray();
        }
    }
}
