using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class TimeTagFreeze : MonoBehaviour
{
    [Header("Time Stop Settings")]
    public float timeStopDuration = 5f;
    private PlayerControls controls;
    private bool isTimeStopped = false;
    private Coroutine stopTimer;

    private bool timeAbilityUnlocked = false;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Player.ToggleTime.performed += ctx => TryToggleTime();
    }

    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();

    private void TryToggleTime()
    {
        if (!timeAbilityUnlocked) return; // ❌ Do nothing if not unlocked yet

        if (!isTimeStopped)
            StopTime();
        else
            ResumeTime();
    }

    private void StopTime()
    {
        isTimeStopped = true;

        GameObject[] timeObjects = GameObject.FindGameObjectsWithTag("TimeControlled");
        foreach (GameObject obj in timeObjects)
        {
            TimeControlledObject controller = obj.GetComponent<TimeControlledObject>();
            if (controller != null)
                controller.Freeze();
        }

        Debug.Log("Time STOPPED.");

        if (stopTimer != null) StopCoroutine(stopTimer);
        stopTimer = StartCoroutine(AutoResumeAfterDelay());
    }

    private void ResumeTime()
    {
        isTimeStopped = false;

        GameObject[] timeObjects = GameObject.FindGameObjectsWithTag("TimeControlled");
        foreach (GameObject obj in timeObjects)
        {
            TimeControlledObject controller = obj.GetComponent<TimeControlledObject>();
            if (controller != null)
                controller.Unfreeze();
        }

        Debug.Log("Time RESUMED.");

        if (stopTimer != null)
        {
            StopCoroutine(stopTimer);
            stopTimer = null;
        }
    }

    private IEnumerator AutoResumeAfterDelay()
    {
        yield return new WaitForSecondsRealtime(timeStopDuration);
        ResumeTime();
    }

    // 🔓 Call this from the unlock trigger!
    public void UnlockTimeAbility()
    {
        timeAbilityUnlocked = true;
        Debug.Log("🕰️ Time Control UNLOCKED!");
    }
}
