using UnityEngine;

public class DeathZoneInteractable : AInteractableBehaviour
{
    protected override void OnInteract(Collider2D collision)
    {
        GameManager.GotoSpawnPoint(collision.gameObject);
        GameManager.SetScreenShake(.5f);
    }
}
