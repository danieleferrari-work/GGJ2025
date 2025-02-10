using BaseTemplate;
using UnityEngine;
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

    protected override bool isDontDestroyOnLoad => false;
    
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenu()
    {
        Debug.Log("cambioscena");
        SceneLoader.LoadScene("MainMenu");
    }
}
