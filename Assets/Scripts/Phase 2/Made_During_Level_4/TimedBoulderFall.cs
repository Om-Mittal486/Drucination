using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TimedBoulderFall : MonoBehaviour
{
    public Rigidbody2D boulderRb;         // Boulder Rigidbody
    public float fallDelay = 6f;           // Wait before Boulder falls
    public string nextSceneName;           // Scene to load after shaking
    public float afterFallDelay = 1.5f;    // Wait after fall before shaking
    public float shakeDuration = 0.5f;     // Duration of shake
    public float shakeMagnitude = 0.3f;    // Strength of shake

    void Start()
    {
        StartCoroutine(StartBoulderFall());
    }

    IEnumerator StartBoulderFall()
    {
        yield return new WaitForSeconds(fallDelay);

        // Start Boulder Falling
        if (boulderRb != null)
            boulderRb.bodyType = RigidbodyType2D.Dynamic;

        // Wait for some time after it starts falling
        yield return new WaitForSeconds(afterFallDelay);

        // Start Shaking the World instead of Camera
        WorldShake worldShake = FindObjectOfType<WorldShake>();
        if (worldShake != null)
            StartCoroutine(worldShake.Shake(shakeDuration, shakeMagnitude));

        // Wait till shaking finishes
        yield return new WaitForSeconds(shakeDuration);

        // Load next scene
        SceneManager.LoadScene(nextSceneName);
    }
}
