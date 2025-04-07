using UnityEngine;

public class CameraFollowConstant : MonoBehaviour
{
    public Rigidbody2D playerRb;        // Assign Player Rigidbody2D in Inspector
    public float cameraSpeed = 5f;      // Constant camera movement speed
    public float followThreshold = 0.1f; // Minimum player speed before camera moves

    void LateUpdate()
    {
        if (playerRb == null) return;

        float playerVelocityX = playerRb.linearVelocity.x;

        // Move only if player is moving horizontally beyond threshold
        if (Mathf.Abs(playerVelocityX) > followThreshold)
        {
            float direction = Mathf.Sign(playerVelocityX); // Get direction: -1 (left) or 1 (right)
            Vector3 newPosition = transform.position;
            newPosition.x += direction * cameraSpeed * Time.deltaTime;
            transform.position = newPosition;
        }
    }
}
