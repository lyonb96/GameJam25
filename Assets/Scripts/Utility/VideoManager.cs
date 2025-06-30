using UnityEngine;

public class VideoManager : MonoBehaviour
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

    void OnVideoPrepared(UnityEngine.Video.VideoPlayer vp)
    {
        vp.Play();
        vp.prepareCompleted -= OnVideoPrepared;
        videoPlayer.loopPointReached += EndReached;
        blackBG.SetActive(false);
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        vp.transform.gameObject.SetActive(false);
    }
}
