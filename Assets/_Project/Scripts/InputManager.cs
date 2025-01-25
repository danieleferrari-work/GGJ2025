using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public bool MovePlayer1Left;
    public bool MovePlayer1Right;

    public PlayerInput PlayerInput 
        => GetComponent<PlayerInput>();

    public InputAction FindAction(string actionName)
        => PlayerInput.actions.FindAction(actionName);

    private void Update()
    {
        MovePlayer1Left = FindAction("MovePlayer1Left").IsPressed();
        MovePlayer1Right = FindAction("MovePlayer1Right").IsPressed();
    }
}
