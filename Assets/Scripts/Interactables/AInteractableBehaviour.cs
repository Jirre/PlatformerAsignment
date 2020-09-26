using UnityEngine;

public abstract class AInteractableBehaviour : MonoBehaviour
{
    [SerializeField] private LayerMask _Layers;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & _Layers) != 0)
            OnInteract(collision);
    }

    protected abstract void OnInteract(Collider2D collision);
}
