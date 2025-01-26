using System.Collections;
using System.Collections.Generic;
using BaseTemplate;
using UnityEngine;

public class RoundsManager : Singleton<RoundsManager>
{
    [SerializeField] int totalRounds;
    [SerializeField] GameObject gameoverPanel;

    [SerializeField] GameObject player1Panel;
    [SerializeField] GameObject player1WinsText;
    [SerializeField] List<GameObject> player1Stars;

    [SerializeField] GameObject player2Panel;
    [SerializeField] GameObject player2WinsText;
    [SerializeField] List<GameObject> player2Stars;

    [SerializeField] GameObject VSImage;

    int currentRound;
    int player1Score;
    int player2Score;

    protected override bool isDontDestroyOnLoad => false;

    Arena arena;

    protected override void InitializeInstance()
    {
        base.InitializeInstance();
        arena = FindFirstObjectByType<Arena>();
        player1Panel.SetActive(false);
        player2Panel.SetActive(false);
        VSImage.SetActive(false);
        gameoverPanel.SetActive(false);
    }

    void Start()
    {
        currentRound = PlayerPrefs.GetInt("CurrentRound", 1);
        player1Score = PlayerPrefs.GetInt("Player1Score", 0);
        player2Score = PlayerPrefs.GetInt("Player2Score", 0);
        StartCoroutine(ShowRoundPanel(true, false, false));
    }

    public void SetRoundLoser(int loserIndex)
    {
        bool player1Wins = loserIndex != 1;
        if (player1Wins)
        {
            Player1Wins();
        }
        else
        {
            Player2Wins();
        }

        NextRound(player1Wins, !player1Wins);
    }

    public void NextRound(bool player1Wins, bool player2Wins)
    {
        currentRound++;
        PlayerPrefs.SetInt("CurrentRound", currentRound);

        if (currentRound > totalRounds)
        {
            bool player1WinsGame = player1Score > player2Score;
            EndGame(player1WinsGame, !player1WinsGame);
        }
        else
        {
            arena.ResetEquilibrium();
            PlayersManager.instance.Reset();
            StartCoroutine(ShowRoundPanel(false, player1Wins, player2Wins));
        }
    }

    private void EndGame(bool player1Wins, bool player2Wins)
    {
        GameManager.instance.gamePaused = true;
        gameoverPanel.SetActive(true);
        player1Panel.SetActive(player1Wins);
        player2Panel.SetActive(player2Wins);
        player1WinsText.SetActive(player1Wins);
        player2WinsText.SetActive(player2Wins);
        
        arena.ResetEquilibrium();
        PlayersManager.instance.Reset();
        PlayerPrefs.DeleteAll();
    }

    private IEnumerator ShowRoundPanel(bool showVS, bool player1Wins, bool player2Wins)
    {
        player1Panel.SetActive(true);
        player2Panel.SetActive(true);
        VSImage.SetActive(showVS);
        player1WinsText.SetActive(player1Wins);
        player2WinsText.SetActive(player2Wins);

        GameManager.instance.gamePaused = true;

        yield return new WaitForSecondsRealtime(3);

        GameManager.instance.gamePaused = false;

        player1Panel.SetActive(false);
        player2Panel.SetActive(false);
        player1WinsText.SetActive(false);
        player2WinsText.SetActive(false);
        VSImage.SetActive(false);
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

    public void OnRestartButtonClicked()
    {
        GameManager.instance.RestartGame();
    }

    public void OnMainMenuButtonClicked()
    {
        GameManager.instance.LoadMainMenu();
    }
}
