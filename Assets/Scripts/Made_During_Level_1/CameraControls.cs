using UnityEngine;

public class CameraFollowConstant : MonoBehaviour
{
    public Rigidbody2D playerRb;
    public float cameraSpeed = 5f;
    public float followThreshold = 0.1f;
    public bool freezeCamera = false;

    public Vector2 stopPosition = new Vector2(6.152573f, -0.06f); // Final stop point
    private bool reachedStopPoint = false;

    public float minX = 0f; // Camera won't go left of this

    public void SnapToPosition(Vector3 position)
    {
        transform.position = new Vector3(position.x, position.y, transform.position.z);
    }

    void LateUpdate()
    {
        if (playerRb == null || freezeCamera || reachedStopPoint) return;

        float playerVelocityX = playerRb.linearVelocity.x;

        if (Mathf.Abs(playerVelocityX) > followThreshold)
        {
            float direction = Mathf.Sign(playerVelocityX);
            Vector3 newPosition = transform.position;
            newPosition.x += direction * cameraSpeed * Time.deltaTime;

            // Clamp camera from going left of minX
            newPosition.x = Mathf.Max(minX, newPosition.x);

            transform.position = newPosition;
        }

        // Stop moving if stop position reached
        if (transform.position.x >= stopPosition.x && transform.position.y <= stopPosition.y)
        {
            reachedStopPoint = true;
        }
    }
}
