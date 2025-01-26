using BaseTemplate;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// Save variables between scenes
/// </summary>
public class GameManager : Singleton<GameManager>
{
    public float masterVolume = 1;
    public float musicVolume = 1;
    public float sfxVolume = 1;
    public float uiVolume = 1;
    public bool fullScreen = true;
    public bool gamePaused = false;

    public UnityAction<Player> OnGameOver;

    public void RestartGame()
    {
        ResetPlayerPrefs();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        RoundsManager.instance.NextRound();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
