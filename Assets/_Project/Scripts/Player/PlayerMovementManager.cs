using UnityEngine;

public class PlayerMovementManager : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    private InputManager inputManager;
    private Arena arena;
    private GameManager gameManager;
    private int playerIndex;
    private float speed;
    private float currentAngle;

    public void Init(int playerIndex, float speed)
    {
        this.playerIndex = playerIndex;
        this.speed = speed;
        currentAngle = playerIndex == 1 ? 0 : Mathf.PI;
    }

    void Awake()
    {
        inputManager = FindFirstObjectByType<InputManager>();
        arena = FindFirstObjectByType<Arena>();
        gameManager = FindFirstObjectByType<GameManager>();
    }

    void Update()
    {
        HandleMovement();
        UpdatePlayerPosition();
        LookAtOpponent();
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
        float x = arena.Radius * Mathf.Cos(currentAngle);
        float z = arena.Radius * Mathf.Sin(currentAngle);
        var targetPosition = new Vector3(x, playerTransform.position.y, z);
        playerTransform.position = targetPosition;
    }

    private void LookAtOpponent()
    {
        playerTransform.LookAt(gameManager.GetOpponent(playerIndex).transform);
    }
}