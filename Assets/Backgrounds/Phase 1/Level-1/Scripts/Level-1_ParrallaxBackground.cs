using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    public float parallaxFactor; // 0 = no movement, 1 = same as camera

    private Transform cam;
    private Vector3 previousCamPos;

    void Start()
    {
        cam = Camera.main.transform;
        previousCamPos = cam.position;
    }

    void LateUpdate()
    {
        Vector3 delta = cam.position - previousCamPos;
        transform.position += delta * parallaxFactor;
        previousCamPos = cam.position;
    }
}
