using UnityEngine;

public class PlayerMovementManager : MonoBehaviour
{
    [SerializeField] Transform playerTransform;

    private InputManager inputManager;
    private MovementCircle movementCircle;
    private float currentAngle = 0;
    private float speed;

    public void Init(float speed)
    {
        this.speed = speed;
    }

    void Awake()
    {
        inputManager = FindFirstObjectByType<InputManager>();
        movementCircle = FindFirstObjectByType<MovementCircle>();
    }

    void Update()
    {
       if (inputManager.Player1MoveLeft)
       {
           currentAngle -= speed * Time.deltaTime;
       }

       if (inputManager.Player1MoveRight)
       {
           currentAngle += speed * Time.deltaTime;
       }

        // Calcola la nuova posizione del giocatore sulla circonferenza
        float x = movementCircle.Radius * Mathf.Cos(currentAngle);
        float z = movementCircle.Radius * Mathf.Sin(currentAngle);

        // Applica la nuova posizione al giocatore
        playerTransform.position = new Vector3(x, playerTransform.position.y, z);
    }
}
