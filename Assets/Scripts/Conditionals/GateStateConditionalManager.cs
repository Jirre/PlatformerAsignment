using UnityEngine;

using JVFramework.Collection;

public class GateStateConditionalManager : AConditionalManagerBehaviour
{
    [SerializeField] private EGateType _GateType;

    protected override bool GetCurrentState() => GameManager.GetGateState() == _GateType;
}
