using System.Collections;
using System.Collections.Generic;
using BaseTemplate;
using TMPro;
using UnityEngine;

public class RoundsManager : Singleton<RoundsManager>
{
    [SerializeField] int totalRounds;
    [SerializeField] UIGameOver gameoverPanel;
    [SerializeField] GameObject startRoundPanel;
    [SerializeField] List<GameObject> player1Stars;
    [SerializeField] List<GameObject> player2Stars;

    int currentRound;
    int player1Score;
    int player2Score;

    protected override bool isDontDestroyOnLoad => false;

    Arena arena;

    protected override void InitializeInstance()
    {
        base.InitializeInstance();
        arena = FindFirstObjectByType<Arena>();
        startRoundPanel.SetActive(false);
    }

    void Start()
    {
        currentRound = PlayerPrefs.GetInt("CurrentRound", 1);
        player1Score = PlayerPrefs.GetInt("Player1Score", 0);
        player2Score = PlayerPrefs.GetInt("Player2Score", 0);
    }

    public void SetRoundLoser(int loserIndex)
    {
        if (loserIndex == 1)
        {
            Player2Wins();
        }
        else
        {
            Player1Wins();
        }

        NextRound();
    }

    public void NextRound()
    {
        currentRound++;
        PlayerPrefs.SetInt("CurrentRound", currentRound);

        if (currentRound > totalRounds)
        {
            EndGame();
        }
        else
        {
            arena.ResetEquilibrium();
            StartCoroutine(ShowRoundPanel());
        }
    }

    private void EndGame()
    {
        gameoverPanel.ShowGameOverScreen();
        bool player1Wins = player1Score > player2Score;
        gameoverPanel.UpdateWinnerText(player1Wins ? PlayersManager.instance.player1.PlayerName : PlayersManager.instance.player2.PlayerName);

        GameManager.instance.ResetPlayerPrefs();
    }

    private IEnumerator ShowRoundPanel()
    {
        startRoundPanel.SetActive(true);

        GameManager.instance.gamePaused = true;

        yield return new WaitForSecondsRealtime(3);

        GameManager.instance.gamePaused = false;

        startRoundPanel.SetActive(false);
    }

    private void Player1Wins()
    {
        player1Score++;
        PlayerPrefs.SetInt("Player1Score", player1Score);

        for (int i = 0; i < player1Score; i++)
        {
            player1Stars[i].SetActive(true);
        }
    }

    private void Player2Wins()
    {
        player2Score++;
        PlayerPrefs.SetInt("Player2Score", player2Score);

        for (int i = 0; i < player2Score; i++)
        {
            player2Stars[i].SetActive(true);
        }
    }
}
