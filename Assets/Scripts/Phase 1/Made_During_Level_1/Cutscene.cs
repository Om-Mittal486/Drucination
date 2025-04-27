using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CutsceneEndToNextScene : MonoBehaviour
{
    public VideoPlayer videoPlayer;   // Drag your VideoPlayer here
    public int nextSceneBuildIndex;   // Set the next scene's build index

    private void Start()
    {
        if (videoPlayer == null)
            videoPlayer = GetComponent<VideoPlayer>();

        videoPlayer.loopPointReached += OnVideoEnd; // Hook event
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene(nextSceneBuildIndex);
    }
}
