using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public bool Player1MoveLeft;
    public bool Player1MoveRight;
    public bool Player1Attack;

    public PlayerInput PlayerInput 
        => GetComponent<PlayerInput>();

    public InputAction FindAction(string actionName)
        => PlayerInput.actions.FindAction(actionName);

    private void Update()
    {
        Player1MoveLeft = FindAction("Player1MoveLeft").IsPressed();
        Player1MoveRight = FindAction("Player1MoveRight").IsPressed();
        Player1Attack = FindAction("Player1Attack").IsPressed();
    }
}
