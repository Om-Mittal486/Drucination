using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitchTrigger : MonoBehaviour
{
    public int sceneBuildIndex;  // The build index of the scene you want to load

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // Check if the player enters the trigger
        {
            SwitchScene();
        }
    }

    private void SwitchScene()
    {
        // Load the scene using the build index
        SceneManager.LoadScene(sceneBuildIndex);
    }
}
