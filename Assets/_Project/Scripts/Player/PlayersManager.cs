using BaseTemplate;
using UnityEngine;

public class PlayersManager : Singleton<PlayersManager>
{
    public Player player1;
    public Player player2;
    
    [SerializeField] UIGameOver uIGameOver;

    protected override bool isDontDestroyOnLoad => false;

    public Player GetOpponent(int playerIndex)
        => playerIndex == 1 ? player2 : player1;

}
