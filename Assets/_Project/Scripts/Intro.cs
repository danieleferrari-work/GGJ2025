using UnityEngine;
using UnityEngine.Video;

public class Intro : MonoBehaviour
{
    [SerializeField] VideoPlayer player;
    [SerializeField] VideoClip clip;
    [SerializeField] GameObject startButton;
    [SerializeField] Animation anim;

    void Start()
    {
        player.loopPointReached += EndIntro;
    }

    private void EndIntro(VideoPlayer source)
    {
        startButton.SetActive(true);
        source.clip = clip;
        source.time = 0;
        source.Play();
        source.isLooping = true;
        player.loopPointReached -= EndIntro;
    }

    public void QuitIntro()
    {
        startButton.SetActive(false);
        anim.Play();
    }

    public void EndQuitIntro()
    {
        gameObject.SetActive(false);
    }
}
