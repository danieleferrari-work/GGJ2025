using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovementManager : MonoBehaviour
{
    [SerializeField] Transform playerTransform;

    private InputManager inputManager;
    private GameManager gameManager;
    private MovementCircle movementCircle;
    private float currentAngle = 0;
    private int playerIndex;
    private float speed;

    public void Init(int playerIndex, float speed)
    {
        this.playerIndex = playerIndex;
        this.speed = speed;

        currentAngle = playerIndex == 1 ? 0 : Mathf.PI;
    }

    void Awake()
    {
        inputManager = FindFirstObjectByType<InputManager>();
        movementCircle = FindFirstObjectByType<MovementCircle>();
        gameManager = FindFirstObjectByType<GameManager>();
    }

    void Update()
    {
       if (inputManager.GetPlayerMoveLeft(playerIndex))
       {
           currentAngle -= speed * Time.deltaTime;
       }
       else if (inputManager.GetPlayerMoveRight(playerIndex))
       {
           currentAngle += speed * Time.deltaTime;
       }

        // Calcola la nuova posizione del giocatore sulla circonferenza
        float x = movementCircle.Radius * Mathf.Cos(currentAngle);
        float z = movementCircle.Radius * Mathf.Sin(currentAngle);

        // Applica la nuova posizione al giocatore
        playerTransform.position = new Vector3(x, playerTransform.position.y, z);

        // Guarda verso l'avversario
        playerTransform.LookAt(gameManager.GetOpponent(playerIndex).transform);
    }
}
