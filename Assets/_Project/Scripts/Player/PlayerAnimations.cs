using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Player player;

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
        int runningDir = 0;
        if (inputManager.GetPlayerMoveLeft(index))
            runningDir = -1;
        if (inputManager.GetPlayerMoveRight(index))
            runningDir = 1;

        // if (runningDir != 0)
        //     player.transform.localScale = new Vector3(runningDir, 1, 1);

        animator.SetBool("isRunning", runningDir != 0);

    }
}
