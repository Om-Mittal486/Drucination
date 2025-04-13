using UnityEngine;

public class BridgeResetTrigger : MonoBehaviour
{
    public BridgeTrigger bridgeTrigger;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            bridgeTrigger.ResetBridge();
        }
    }
}