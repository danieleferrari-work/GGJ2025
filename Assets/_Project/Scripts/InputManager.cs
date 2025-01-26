using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private bool Player1MoveLeft;
    private bool Player1MoveRight;
    private bool Player1Attack;

    private bool Player2MoveLeft;
    private bool Player2MoveRight;
    private bool Player2Attack;


    public bool GetPlayerMoveLeft(int playerIndex)
        => playerIndex == 1 ? Player1MoveLeft : Player2MoveLeft;

    public bool GetPlayerMoveRight(int playerIndex)
        => playerIndex == 1 ? Player1MoveRight : Player2MoveRight;

    public bool GetPlayerAttack(int playerIndex)
        => playerIndex == 1 ? Player1Attack : Player2Attack;


    private PlayerInput PlayerInput
        => GetComponent<PlayerInput>();

    private InputAction FindAction(string actionName)
        => PlayerInput.actions.FindAction(actionName);

    private void Update()
    {
        Player1MoveLeft = FindAction("Player1MoveLeft").IsPressed() && !GameManager.instance.gamePaused;
        Player1MoveRight = FindAction("Player1MoveRight").IsPressed() && !GameManager.instance.gamePaused;
        Player1Attack = FindAction("Player1Attack").IsPressed() && !GameManager.instance.gamePaused;

        Player2MoveLeft = FindAction("Player2MoveLeft").IsPressed() && !GameManager.instance.gamePaused;
        Player2MoveRight = FindAction("Player2MoveRight").IsPressed() && !GameManager.instance.gamePaused;
        Player2Attack = FindAction("Player2Attack").IsPressed() && !GameManager.instance.gamePaused;
    }
}
