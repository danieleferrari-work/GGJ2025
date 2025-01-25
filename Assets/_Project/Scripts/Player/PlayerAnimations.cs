using UnityEditor.Animations;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] Animator animator;

    int index;
    InputManager inputManager;

    private void Awake()
    {
        inputManager = FindFirstObjectByType<InputManager>();
    }

    public void Init(int index)
    {
        this.index = index;
    }

    private void Update()
    {
        bool isRunning = inputManager.GetPlayerMoveLeft(index) || inputManager.GetPlayerMoveRight(index);
        Debug.Log(isRunning);
        animator.SetBool("isRunning", isRunning);

    }
}
