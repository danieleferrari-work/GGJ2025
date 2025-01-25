using TMPro;
using UnityEngine;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] TMP_Text winnerText;

    void Awake()
    {
        gameObject.SetActive(false);
        GameManager.instance.OnGameOver += ShowGameOverScreen;
    }

    void OnDestroy()
    {
        Time.timeScale = 1f;
        GameManager.instance.OnGameOver -= ShowGameOverScreen;
    }

    private void ShowGameOverScreen(Player loser)
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
        UpdateWinnerText(PlayersManager.instance.GetOpponent(loser.Index).PlayerName);
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
