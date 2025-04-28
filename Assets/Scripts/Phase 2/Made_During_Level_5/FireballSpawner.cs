using UnityEngine;
using System.Collections;

[System.Serializable]
public class SpawnRange
{
    public float minY;
    public float maxY;
}

public class FireballSpawner : MonoBehaviour
{
    public static FireballSpawner instance;
    public static bool globalSlowActive = false;

    public GameObject fireballPrefab;
    public float spawnInterval = 2f;
    public float fireballSpeed = 5f;
    public float fireballSlowSpeed = 2f;
    public float slowDuration = 3f; // Time in seconds for slowdown
    public SpawnRange[] spawnRanges;
    private bool slowed = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        InvokeRepeating(nameof(SpawnFireball), 1f, spawnInterval);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (GemManager.instance.gemsCollected > 0 && !slowed)
            {
                GemManager.instance.UseGem();
                StartCoroutine(SlowDownFireballsTemporarily());
            }
        }
    }

    IEnumerator SlowDownFireballsTemporarily()
    {
        slowed = true;
        globalSlowActive = true;
        yield return new WaitForSeconds(slowDuration);
        slowed = false;
        globalSlowActive = false;
    }

    void SpawnFireball()
    {
        if (spawnRanges.Length == 0) return;

        // Pick random range
        SpawnRange selectedRange = spawnRanges[Random.Range(0, spawnRanges.Length)];
        float randomY = Random.Range(selectedRange.minY, selectedRange.maxY);

        Vector3 spawnPos = new Vector3(transform.position.x, randomY, 0f);
        GameObject fireball = Instantiate(fireballPrefab, spawnPos, Quaternion.identity);

        FireballLogic logic = fireball.GetComponent<FireballLogic>();
        if (logic != null)
        {
            logic.speed = slowed ? fireballSlowSpeed : fireballSpeed;
        }
    }
}
