using UnityEngine;

public class TimeControlledObject : MonoBehaviour
{
    private Rigidbody2D rb;
    private MonoBehaviour[] allScripts;
    private Vector2 storedVelocity;
    private float storedGravity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        allScripts = GetComponents<MonoBehaviour>();
    }

    public void Freeze()
    {
        if (rb != null)
        {
            storedVelocity = rb.linearVelocity;
            storedGravity = rb.gravityScale;

            rb.linearVelocity = Vector2.zero;
            rb.gravityScale = 0f;
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.simulated = false;
        }

        foreach (MonoBehaviour script in allScripts)
        {
            if (script != this)
                script.enabled = false;
        }

        // Lock Z
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
    }

    public void Unfreeze()
    {
        if (rb != null)
        {
            rb.simulated = true;
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = storedGravity;

            rb.linearVelocity = storedVelocity;

            // Clamp excessive velocities
            if (rb.linearVelocity.magnitude > 20f)
                rb.linearVelocity = rb.linearVelocity.normalized * 10f;
        }

        foreach (MonoBehaviour script in allScripts)
        {
            if (script != this)
                script.enabled = true;
        }

        // Lock Z again just in case
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
    }
}