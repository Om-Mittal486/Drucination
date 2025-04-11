using UnityEngine;

public class CameraFollowLocked : MonoBehaviour
{
    public Transform target;                         // Assign your player here
    public Vector3 offset = new Vector3(0, 0, -10);   // Z offset for 2D camera

    public float minX = 1.173038f;
    public float maxX = 47f;
    public float minY = 0.09f;
    public float maxY = 5.21f;

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;

            // Clamp X between min and max
            float clampedX = Mathf.Clamp(desiredPosition.x, minX, maxX);

            // Clamp Y between min and max
            float clampedY = Mathf.Clamp(desiredPosition.y, minY, maxY);

            // Apply final clamped position
            transform.position = new Vector3(clampedX, clampedY, desiredPosition.z);
        }
    }
}
