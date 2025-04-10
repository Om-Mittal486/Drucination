using UnityEngine;

public class CameraFollowConstant : MonoBehaviour
{
    public Rigidbody2D playerRb;
    public float cameraSpeed = 5f;
    public float followThreshold = 0.1f;
    public bool freezeCamera = false;

    public float stopX = 5.152573f;
    public float stopY = -0.06f;

    void LateUpdate()
    {
        if (playerRb == null || freezeCamera) return;

        float playerVelocityX = playerRb.linearVelocity.x;

        if (Mathf.Abs(playerVelocityX) > followThreshold)
        {
            float direction = Mathf.Sign(playerVelocityX);
            Vector3 newPosition = transform.position;
            newPosition.x += direction * cameraSpeed * Time.deltaTime;
            transform.position = newPosition;
        }

        // Check if camera reached the stop point
        if (transform.position.x >= stopX && transform.position.y <= stopY)
        {
            freezeCamera = true;
        }
    }
}
