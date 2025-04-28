using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonHandler : MonoBehaviour
{
    public int sceneToLoadIndex = 1; // Set this to your target scene build index

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoadIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game pressed."); // Works only in build
    }
}

