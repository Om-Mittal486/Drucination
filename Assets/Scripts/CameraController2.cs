using UnityEngine;
using System.Collections;

public class CameraFollowBounded : MonoBehaviour
{
    [Header("Target & Offset")]
    public Transform target;
    public Vector3 offset = new Vector3(0, 0, -10);

    [Header("Camera Limits")]
    public float minX = 1.173038f;
    public float maxX = 47f;
    public float minY = 0.09f;
    public float maxY = 5.21f;

    [Header("Cinematic Movement")]
    public float cinematicSpeed = 3f;
    private bool cinematicRunning = false;

    void LateUpdate()
    {
        if (target == null || cinematicRunning) return;

        Vector3 desiredPos = target.position + offset;

        // Clamp to limits
        desiredPos.x = Mathf.Clamp(desiredPos.x, minX, maxX);
        desiredPos.y = Mathf.Clamp(desiredPos.y, minY, maxY);

        transform.position = desiredPos;
    }

    public void StartParkourView()
    {
        if (!cinematicRunning)
            StartCoroutine(ParkourViewRoutine());
    }

    private IEnumerator ParkourViewRoutine()
    {
        cinematicRunning = true;

        // Lock player movement
        if (target.TryGetComponent<PlayerController>(out var pc))
            pc.canMove = false;

        // Step 1: Move up to y = 3
        Vector3 step1 = new Vector3(transform.position.x, 3f, transform.position.z);
        yield return StartCoroutine(MoveToPosition(step1));

        // Step 2: Move right to x = 47
        Vector3 step2 = new Vector3(47f, transform.position.y, transform.position.z);
        yield return StartCoroutine(MoveToPosition(step2));

        // Step 3: Return to player QUICKLY
        Vector3 step3 = new Vector3(
            Mathf.Clamp(target.position.x + offset.x, minX, maxX),
            Mathf.Clamp(target.position.y + offset.y, minY, maxY),
            transform.position.z
        );
        yield return StartCoroutine(MoveToPosition(step3, cinematicSpeed * 4f)); // 3x speed

        // Re-enable player movement and trigger jump
        if (pc != null)
            pc.UnlockMovementWithJump();

        cinematicRunning = false;
    }

    private IEnumerator MoveToPosition(Vector3 targetPos, float speedOverride = -1f)
    {
        float moveSpeed = speedOverride > 0 ? speedOverride : cinematicSpeed;

        while (Vector3.Distance(transform.position, targetPos) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.unscaledDeltaTime);
            yield return null;
        }

        transform.position = targetPos;
    }
}
