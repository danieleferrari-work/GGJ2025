using BaseTemplate.Attributes;
using BaseTemplate.UIControl;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class MainMenuUI : MonoBehaviour
{
    [SceneInstance][SerializeField] string playScene;
    [Space]
    [SerializeField] OptimizeEventSystem eventSystemController;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject howToPlay;
    [SerializeField] CanvasGroup fader;
    [SerializeField] OptionsUI optionsMenu;
    [SerializeField] AudioSource music;
    [SerializeField] VideoPlayer howToPlayVideo;

    [Header("Auto register to events")]
    [SerializeField] Button playButton;
    [SerializeField] Button optionsButton;
    [SerializeField] Button backFromOptionsButton;
    [SerializeField] Button quitButton;

    private void Awake()
    {
        howToPlayVideo.loopPointReached += (VideoPlayer video) => OnEndHowToPlay();
        music.DOFade(.2f, .5f);
        music.Play();
        //Auto register to events
        if (playButton) playButton.onClick.AddListener(OnClickPlay);
        if (optionsButton) optionsButton.onClick.AddListener(OnClickOptions);
        if (backFromOptionsButton) backFromOptionsButton.onClick.AddListener(OnClickBackToMainMenu);
        if (quitButton) quitButton.onClick.AddListener(OnClickQuit);
    }

    private void OnDestroy()
    {
        //unregister events
        if (playButton) playButton.onClick.RemoveListener(OnClickPlay);
        if (optionsButton) optionsButton.onClick.RemoveListener(OnClickOptions);
        if (backFromOptionsButton) backFromOptionsButton.onClick.RemoveListener(OnClickBackToMainMenu);
        if (quitButton) quitButton.onClick.RemoveListener(OnClickQuit);
    }

    public void OnClickPlay()
    {
        fader.DOFade(1, .5f).onComplete += () =>
        {
            howToPlay.SetActive(true);
            fader.DOFade(0, .1f);
        };
        music.DOFade(0, .5f).onComplete += () => music.Stop();
    }

    public void OnEndHowToPlay()
    {
        fader.DOFade(1, .5f).onComplete += () => SceneLoader.LoadScene(playScene);
    }

    public void OnClickOptions()
    {
        eventSystemController.ChangeMenu(optionsMenu.gameObject);
        optionsMenu.UpdateUI();
    }

    public void OnClickBackToMainMenu()
    {
        eventSystemController.ChangeMenu(mainMenu);
    }

    public void OnClickQuit()
    {
        mainMenu.SetActive(false);
        OnClickConfirmOnPopup();
    }

    public void OnClickCancelOnPopup()
    {
        mainMenu.SetActive(true);
    }

    public void OnClickConfirmOnPopup()
    {
        SceneLoader.ExitGame();
    }
}
