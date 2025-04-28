using UnityEngine;
using System.Collections;

public class WorldShake : MonoBehaviour
{
    public Transform worldTransform; // Drag the "World" GameObject here

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = worldTransform.localPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            worldTransform.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        worldTransform.localPosition = originalPos;
    }
}
