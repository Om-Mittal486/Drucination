using UnityEngine;

public class RespawnTrigger : MonoBehaviour
{
    [Header("Set the position where the player will respawn")]
    public Transform respawnPoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && respawnPoint != null)
        {
            other.transform.position = respawnPoint.position;

            // Reset velocity if player has Rigidbody2D
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.linearVelocity = Vector2.zero;
        }
    }
}
