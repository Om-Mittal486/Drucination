using UnityEngine;

public class SpikeMover : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveDistance = 1f;         // How far the spike moves up
    public float moveSpeed = 10f;           // How fast it moves up/down
    public float waitAtBottom = 0.5f;       // Pause time when hidden
    public float waitAtTop = 0.2f;          // Pause time when up

    private Vector3 bottomPos;
    private Vector3 topPos;
    private Vector3 targetPos;
    private float timer = 0f;
    private bool waiting = false;

    private void Start()
    {
        bottomPos = transform.position;
        topPos = new Vector3(bottomPos.x, bottomPos.y + moveDistance, bottomPos.z);
        targetPos = topPos; // Start by going up
    }

    private void Update()
    {
        if (!waiting)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPos) < 0.01f)
            {
                waiting = true;
                timer = (targetPos == topPos) ? waitAtTop : waitAtBottom;
            }
        }
        else
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                targetPos = (targetPos == topPos) ? bottomPos : topPos;
                waiting = false;
            }
        }
    }
}
