using UnityEngine;
using UnityEngine.SceneManagement;

public class FireballLogic : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 5f;
    private bool globallySlowed = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }
        rb.gravityScale = 0;
        rb.linearVelocity = Vector2.left * speed;
    }

    void Update()
    {
        if (FireballSpawner.globalSlowActive && !globallySlowed)
        {
            rb.linearVelocity = Vector2.left * FireballSpawner.instance.fireballSlowSpeed;
            globallySlowed = true;
        }
        else if (!FireballSpawner.globalSlowActive && globallySlowed)
        {
            rb.linearVelocity = Vector2.left * FireballSpawner.instance.fireballSpeed;
            globallySlowed = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(collision.gameObject); // Destroy Player
            Application.Quit();            // Quit the Game
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}
