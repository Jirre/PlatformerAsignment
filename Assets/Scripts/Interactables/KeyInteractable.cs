using UnityEngine;

public class KeyInteractable : AInteractableBehaviour
{
    [SerializeField] private EGateType _GateType;
    protected override void OnInteract(Collider2D collision)
    {
        GameManager.SetGateState(_GateType);
        GameManager.SetSpawnPoint(transform);
        GameManager.SetScreenShake(.1f);
    }
}
