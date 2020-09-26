using System.Collections.Generic;
using UnityEngine;

namespace JVFramework.Collection
{
    [AddComponentMenu("JVFramework/Conditionals/Listeners/GameObject Activator")]
    internal class ConditionalGameObjectActivator : AConditionalListener
    {
        [SerializeField] private List<GameObject> _OnTrueGameObjects;
        [SerializeField] private List<GameObject> _OnFalseGameObjects;

        internal override void OnTrue()
        {
            if ((_OnTrueGameObjects?.Count ?? 0) > 0) foreach (GameObject o in _OnTrueGameObjects)
                    if (o != null) o.SetActive(true);
            if ((_OnFalseGameObjects?.Count ?? 0) > 0) foreach (GameObject o in _OnFalseGameObjects)
                    if (o != null) o.SetActive(false);
        }
        internal override void OnFalse()
        {
            if ((_OnTrueGameObjects?.Count ?? 0) > 0) foreach (GameObject o in _OnTrueGameObjects)
                    if (o != null) o.SetActive(false);
            if ((_OnFalseGameObjects?.Count ?? 0) > 0) foreach (GameObject o in _OnFalseGameObjects)
                    if (o != null) o.SetActive(true);
        }
    }
}
