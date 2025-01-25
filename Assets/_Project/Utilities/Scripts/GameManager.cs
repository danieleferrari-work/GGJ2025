using BaseTemplate;
using UnityEngine;

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
    public Player player1;
    public Player player2;

    [Range(0, 1)]
    public float equilibrium;


    public Player GetOpponent(int playerIndex)
        => playerIndex == 1 ? player2 : player1;

    public void AlterEquilibrium(int playerIndex, float value)
    {
        equilibrium = playerIndex == 1 ? equilibrium + value : equilibrium - value;
        equilibrium = Mathf.Clamp(equilibrium, 0, 1);
    }
}
