using UnityEngine;
using System.Collections;

public class TimeRewindRespawn : MonoBehaviour
{
    public Transform respawnPoint;
    public BridgeTrigger bridgeTrigger;
    public float rewindDelay = 0.5f;
    public float timeSlowFactor = 0.4f;
    public float slowDuration = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(RewindAndRespawn(other.gameObject));
        }
    }

    private IEnumerator RewindAndRespawn(GameObject player)
    {
        // 1. Disable player control
        PlayerController pc = player.GetComponent<PlayerController>();
        if (pc != null) pc.canMove = false;

        // 2. Slow down time
        Time.timeScale = timeSlowFactor;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        // 3. Reset the bridge
        bridgeTrigger.ResetBridge();

        // 4. Wait for bridge rewind (duration matches your rewind block duration)
        yield return new WaitForSecondsRealtime(1f); // Adjust if needed

        // 5. Respawn player (keep camera still for now)
        player.transform.position = respawnPoint.position;
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb != null) rb.linearVelocity = Vector2.zero;

        // 6. Wait a bit to let the moment land
        yield return new WaitForSecondsRealtime(0.5f);

        // 7. Snap camera to player
        Camera.main.transform.position = new Vector3(
            respawnPoint.position.x,
            respawnPoint.position.y,
            Camera.main.transform.position.z
        );

        // 8. Restore time
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;

        // 9. Enable movement
        yield return new WaitForSeconds(0.1f);
        if (pc != null) pc.canMove = true;
    }
}
