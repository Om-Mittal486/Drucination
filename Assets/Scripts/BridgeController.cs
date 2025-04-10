using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BridgeTrigger : MonoBehaviour
{
    public List<BridgeBlock> bridgeBlocks = new List<BridgeBlock>();
    public float fallInterval = 1.5f;
    public float shakeDuration = 0.12f;
    public float shakeMagnitude = 0.01f;
    public string playerTag = "Player";

    private bool triggered = false;
    private Vector3 originalCamPos;
    private Coroutine fallRoutine;

    private void Start()
    {
        foreach (BridgeBlock block in bridgeBlocks)
        {
            block.startPos = block.rb.transform.position;
            block.startRot = block.rb.transform.rotation;
            block.rb.bodyType = RigidbodyType2D.Kinematic;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!triggered && other.CompareTag(playerTag))
        {
            triggered = true;
            fallRoutine = StartCoroutine(StartBridgeFall());
        }
    }

    private IEnumerator StartBridgeFall()
    {
        yield return new WaitForSeconds(1f);

        foreach (BridgeBlock block in bridgeBlocks)
        {
            yield return StartCoroutine(ShakeAndDrop(block));
            yield return new WaitForSeconds(fallInterval);
        }
    }

    private IEnumerator ShakeAndDrop(BridgeBlock block)
    {
        Transform t = block.rb.transform;
        Vector3 originalPos = t.position;
        float timer = 0f;

        while (timer < shakeDuration)
        {
            float offsetX = Random.Range(-shakeMagnitude, shakeMagnitude);
            float offsetY = Random.Range(-shakeMagnitude, shakeMagnitude);
            t.position = originalPos + new Vector3(offsetX, offsetY, 0f);
            timer += Time.deltaTime;
            yield return null;
        }

        t.position = originalPos;
        block.rb.bodyType = RigidbodyType2D.Dynamic;
    }

    public void ResetBridge()
    {
        if (fallRoutine != null)
        {
            StopCoroutine(fallRoutine);
            fallRoutine = null;
        }

        foreach (BridgeBlock block in bridgeBlocks)
        {
            StartCoroutine(RewindBlock(block));
        }

        triggered = false;
    }

    private IEnumerator RewindBlock(BridgeBlock block)
    {
        float duration = 0.5f;
        float t = 0f;

        Vector3 start = block.rb.transform.position;
        Quaternion rotStart = block.rb.transform.rotation;

        block.rb.linearVelocity = Vector2.zero;
        block.rb.angularVelocity = 0;
        block.rb.bodyType = RigidbodyType2D.Kinematic;

        while (t < duration)
        {
            float percent = t / duration;
            block.rb.transform.position = Vector3.Lerp(start, block.startPos, percent);
            block.rb.transform.rotation = Quaternion.Lerp(rotStart, block.startRot, percent);
            t += Time.deltaTime;
            yield return null;
        }

        block.rb.transform.position = block.startPos;
        block.rb.transform.rotation = block.startRot;
    }
}
