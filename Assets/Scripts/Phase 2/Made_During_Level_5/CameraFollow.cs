using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;    // Drag your Player here
    public Vector3 offset;      // Camera offset from player
    public float smoothSpeed = 5f;

    [Header("Camera Bounds")]
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;

        // Clamp within bounds
        float clampedX = Mathf.Clamp(desiredPosition.x, minX, maxX);
        float clampedY = Mathf.Clamp(desiredPosition.y, minY, maxY);

        Vector3 clampedPosition = new Vector3(clampedX, clampedY, transform.position.z);

        // Smoothly move camera
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, clampedPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
