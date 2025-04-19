using UnityEngine;

public class ParkourTrigger : MonoBehaviour
{
    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;
            CameraFollowBounded cam = Camera.main.GetComponent<CameraFollowBounded>();
            if (cam != null)
                cam.StartParkourView();

            Destroy(gameObject); // One-time trigger
        }
    }
}
