using BaseTemplate;
using TMPro;
using UnityEngine;

public class RoundsManager : Singleton<RoundsManager>
{
    [SerializeField] int totalRounds;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text currentRoundText;
    [SerializeField] UIGameOver gameoverPanel;
    [SerializeField] GameObject startRoundPanel;

    int currentRound;
    int player1Score;
    int player2Score;


    public void NextRound()
    {
        currentRound++;
        currentRoundText.text = $"Round {currentRound}/{totalRounds}";

        if (currentRound == totalRounds)
        {
            gameoverPanel.ShowGameOverScreen();
            gameoverPanel.UpdateWinnerText(player1Score > player2Score ? PlayersManager.instance.player1.PlayerName : PlayersManager.instance.player2.PlayerName);
        }
        else
        {
            startRoundPanel.SetActive(true);
        }

        SceneLoader.LoadScene("MainScene");
    }

    public void SetRoundLoser(int loserIndex)
    {
        if (loserIndex == 1)
        {
            player2Score++;
        }
        else
        {
            player1Score++;
        }

        scoreText.text = $"{player1Score} - {player2Score}";
    }
}
