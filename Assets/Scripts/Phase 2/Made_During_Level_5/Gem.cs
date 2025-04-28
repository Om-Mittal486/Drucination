using UnityEngine;

public class Gem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GemManager.instance.CollectGem();
            Destroy(gameObject);
        }
    }
}
