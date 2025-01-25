using TMPro;
using UnityEngine;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] TMP_Text winnerText;

    void Awake()
    {
        gameObject.SetActive(false);
    }

    void OnDestroy()
    {
        Time.timeScale = 1f;
    }

    public void ShowGameOverScreen()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void UpdateWinnerText(string winnerName)
    {
        winnerText.text = $"{winnerName} wins!";
    }

    public void OnRestartButtonClicked()
    {
        GameManager.instance.RestartGame();
    }

    public void OnMainMenuButtonClicked()
    {
        GameManager.instance.LoadMainMenu();
    }
}
