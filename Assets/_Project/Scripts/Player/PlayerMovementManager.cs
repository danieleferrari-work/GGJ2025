using UnityEngine;

public class PlayerMovementManager : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    private InputManager inputManager;
    private Arena arena;
    private int playerIndex;
    private float speed;
    private float currentAngle;

    public void Init(int playerIndex, float speed)
    {
        this.playerIndex = playerIndex;
        this.speed = speed;
        Reset();
    }

    void Awake()
    {
        inputManager = FindFirstObjectByType<InputManager>();
        arena = FindFirstObjectByType<Arena>();
    }

    void Update()
    {
        HandleMovement();
        UpdatePlayerPosition();
        LookAtOpponent();
    }

    public void Reset()
    {
        currentAngle = playerIndex == 1 ? 0 : Mathf.PI;
    }

    private void HandleMovement()
    {
        float targetAngle = currentAngle;
        if (inputManager.GetPlayerMoveLeft(playerIndex))
        {
            targetAngle -= speed * Time.deltaTime;
        }
        else if (inputManager.GetPlayerMoveRight(playerIndex))
        {
            targetAngle += speed * Time.deltaTime;
        }

        currentAngle = arena.ClampPlayerAngle(playerIndex, targetAngle);
    }

    private void UpdatePlayerPosition()
    {
        playerTransform.position = CircleUtils.GetPositionBasedOnAngle(arena.transform.position, arena.Radius, currentAngle);
    }

    private void LookAtOpponent()
    {
        playerTransform.LookAt(PlayersManager.instance.GetOpponent(playerIndex).transform);
    }
}