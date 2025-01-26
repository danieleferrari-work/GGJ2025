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
        GameManager.instance.gamePaused = false;
    }

    public void ShowGameOverScreen()
    {
        gameObject.SetActive(true);
        GameManager.instance.gamePaused = true;
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
