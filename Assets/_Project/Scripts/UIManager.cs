using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TMP_Text player1LifeText;
    [SerializeField] TMP_Text player2LifeText;


    void Start()
    {
        Player.OnLifeChange += OnLifeChange;
    }

    void OnDestroy()
    {
        Player.OnLifeChange -= OnLifeChange;
    }

    private void OnLifeChange(int playerIndex, int life)
    {
        if (playerIndex == 1)
        {
            player1LifeText.text = life.ToString();
        }
        else if (playerIndex == 2)
        {
            player2LifeText.text = life.ToString();
        }
    }
}
