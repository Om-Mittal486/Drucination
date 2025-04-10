using UnityEngine;

public class CameraFollowConstant : MonoBehaviour
{
    public Rigidbody2D playerRb;
    public float cameraSpeed = 5f;
    public float followThreshold = 0.1f;
    public bool freezeCamera = false;

    public Vector2 stopPosition = new Vector2(6.152573f, -0.06f); // ← your final stop
    private bool reachedStopPoint = false;

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
            transform.position = newPosition;
        }

        // Check if we've reached or passed the stop point
        if (transform.position.x >= stopPosition.x && transform.position.y <= stopPosition.y)
        {
            reachedStopPoint = true;
        }
    }
}
