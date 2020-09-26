using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JVFramework.Collection
{
    public abstract class AConditionalManagerBehaviour : MonoBehaviour
    {
        public ConditionalManager ConditionalManager;
        public bool FindInChildren;

        protected bool _currentState;
        public bool CurrentState { get => _currentState; }

        public void SetState(bool pState)
        {
            _currentState = pState;
            OnStateChange(pState);
            if (ConditionalManager == null) ConditionalManager = new ConditionalManager(FindInChildren);
            else ConditionalManager.FindInChildren = FindInChildren;
            ConditionalManager.SetState(gameObject, pState);
        }

        public void ToggleState() => SetState(!_currentState);

        public virtual void OnEnable()
        {
            _currentState = GetCurrentState();
            if (ConditionalManager == null) ConditionalManager = new ConditionalManager(FindInChildren);
            else ConditionalManager.FindInChildren = FindInChildren;
            ConditionalManager.SetState(gameObject, _currentState);
        }
        public virtual void OnDisable() { }

        protected virtual bool GetCurrentState() => _currentState;
        protected virtual void OnStateChange(bool pState) { }

        private void Update()
        {
            if (_currentState != GetCurrentState())
                SetState(GetCurrentState());
        }
    }
}