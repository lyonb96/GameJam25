using UnityEngine;

public class CreditsPlayer : MonoBehaviour
{
    public UnityEngine.Video.VideoPlayer videoPlayer;

    [SerializeField] GameObject blackBG;

    void Start()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Prepare();
            videoPlayer.prepareCompleted += OnVideoPrepared;
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            PlayVideo();
        }
    }

    bool isPrepared = false;

    void OnVideoPrepared(UnityEngine.Video.VideoPlayer vp)
    {
        isPrepared = true;
        videoPlayer.prepareCompleted -= OnVideoPrepared;
        // Do not play yet, wait for PlayVideo to be called
    }

    public void PlayVideo()
    {
        if (isPrepared)
        {
            videoPlayer.transform.gameObject.SetActive(true);
            videoPlayer.Play();
            videoPlayer.loopPointReached += EndReached;
            blackBG.SetActive(false);
        }
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }
}
