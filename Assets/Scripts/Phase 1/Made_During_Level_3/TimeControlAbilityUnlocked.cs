using UnityEngine;
using System.Collections;

public class TimeStopAbilityUnlockTrigger : MonoBehaviour
{
    public Vector3 teleportPosition = new Vector3(7.1f, -1.97f, 0f);
    public float delayBeforeTeleport = 3f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TimeTagFreeze timeFreeze = other.GetComponent<TimeTagFreeze>();
            if (timeFreeze != null)
            {
                timeFreeze.UnlockTimeAbility();

                // Disappear visually
                GetComponent<SpriteRenderer>().enabled = false;
                Collider2D col = GetComponent<Collider2D>();
                if (col != null) col.enabled = false;

                StartCoroutine(DelayedTeleport(other.gameObject));
            }
        }
    }

    private IEnumerator DelayedTeleport(GameObject player)
    {
        yield return new WaitForSecondsRealtime(delayBeforeTeleport);

        if (player != null)
            player.transform.position = teleportPosition;

        Destroy(gameObject); // Destroy trigger after teleport
    }
}