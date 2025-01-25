using UnityEngine;

public class Player1MovementManager : MonoBehaviour
{
    [SerializeField] float speed;

    private InputManager inputManager;
    private MovementCircle movementCircle;
    private float currentAngle = 0;


    void Awake()
    {
        inputManager = FindFirstObjectByType<InputManager>();
        movementCircle = FindFirstObjectByType<MovementCircle>();
    }

    void Update()
    {
       if (inputManager.MovePlayer1Left)
       {
           Debug.Log("MovePlayer1Left");
           currentAngle -= speed * Time.deltaTime;
       }

       if (inputManager.MovePlayer1Right)
       {
           Debug.Log("MovePlayer1Right");
           currentAngle += speed * Time.deltaTime;
       }

        // Calcola la nuova posizione del giocatore sulla circonferenza
        float x = movementCircle.Radius * Mathf.Cos(currentAngle);
        float z = movementCircle.Radius * Mathf.Sin(currentAngle);

        // Applica la nuova posizione al giocatore
        transform.position = new Vector3(x, transform.position.y, z);
    }
}
