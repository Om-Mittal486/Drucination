using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;

    private Vector3 targetPosition;
    private Vector3 lastPosition;

    void Start()
    {
        if (pointA != null && pointB != null)
        {
            transform.position = pointA.position;
            targetPosition = pointB.position;
        }

        lastPosition = transform.position;
    }

    void Update()
    {
        if (pointA == null || pointB == null) return;

        // Move platform
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Switch direction at end
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            targetPosition = (targetPosition == pointA.position) ? pointB.position : pointA.position;
        }
    }

    void LateUpdate()
    {
        lastPosition = transform.position;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Only move the player if they are standing on top of the platform
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y > 0.5f) // standing on top
                {
                    Vector3 platformMovement = transform.position - lastPosition;
                    collision.transform.position += platformMovement;
                    break;
                }
            }
        }
    }
}
