using UnityEngine;
using System.Collections;

public class TimeRewindRespawn : MonoBehaviour
{
    public Transform respawnPoint;
    public Transform rewindCameraFocusPoint;
    public BridgeTrigger bridgeTrigger;

    public float rewindDelay = 0.5f;
    public float timeSlowFactor = 0.4f;
    public float slowDuration = 1f;
    public float cameraReturnSpeed = 2f;
    public float jumpAfterRespawnForce = 10f;

    private CameraFollowConstant cameraFollow;

    private void Start()
    {
        cameraFollow = Camera.main.GetComponent<CameraFollowConstant>();
    }

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

        // 2. Freeze camera follow
        if (cameraFollow != null) cameraFollow.freezeCamera = true;

        // 3. Slow down time
        Time.timeScale = timeSlowFactor;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        // 4. Move camera to rewind focus point
        if (rewindCameraFocusPoint != null)
        {
            yield return StartCoroutine(MoveCameraToPosition(rewindCameraFocusPoint.position));
        }

        // 5. Reset the bridge
        bridgeTrigger.ResetBridge();

        // 6. Wait for bridge reset to finish
        yield return new WaitForSecondsRealtime(1f);

        // 7. Respawn player
        player.transform.position = respawnPoint.position;
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb != null) rb.linearVelocity = Vector2.zero;

        // 8. Smooth camera move to player
        yield return StartCoroutine(MoveCameraToPosition(respawnPoint.position));

        // 9. Auto-jump
        if (rb != null)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpAfterRespawnForce);
        }

        // 10. Restore time
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;

        // 11. Wait a little, then enable player
        yield return new WaitForSeconds(0.1f);
        if (pc != null) pc.canMove = true;

        // 12. Snap camera follow position & re-enable follow
        if (cameraFollow != null)
        {
            cameraFollow.SnapToPosition(respawnPoint.position);
            cameraFollow.freezeCamera = false;
        }
    }

    private IEnumerator MoveCameraToPosition(Vector3 targetPosition)
    {
        Transform cam = Camera.main.transform;
        Vector3 startPos = cam.position;
        Vector3 endPos = new Vector3(targetPosition.x, targetPosition.y, cam.position.z);

        float t = 0f;
        while (Vector3.Distance(cam.position, endPos) > 0.05f)
        {
            t += Time.unscaledDeltaTime * cameraReturnSpeed;
            cam.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        cam.position = endPos;
    }
}
