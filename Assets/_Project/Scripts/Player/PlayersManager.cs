using BaseTemplate;

public class PlayersManager : Singleton<PlayersManager>
{
    public Player player1;
    public Player player2;
    
    protected override bool isDontDestroyOnLoad => false;

    public Player GetOpponent(int playerIndex)
        => playerIndex == 1 ? player2 : player1;

    public void Reset()
    {
        player1.Reset();
        player2.Reset();
    }
}
