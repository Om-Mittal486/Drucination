using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    public float floatSpeed = 1f;      // Speed of floating
    public float floatHeight = 0.5f;   // How high it floats

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        float yOffset = Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = startPos + new Vector3(0f, yOffset, 0f);
    }
}
