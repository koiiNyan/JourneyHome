using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class PlayIntro : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer.loopPointReached += OnVideoFinished;
        videoPlayer.Play();
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        SceneManager.LoadScene("Game");
    }

    public void SkipVideo()
    {
        SceneManager.LoadScene("Game");
    }
}