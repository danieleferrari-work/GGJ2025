using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float movementSpeed;
    [SerializeField] float attackDelay;

    public void Awake()
    {
        GetComponentInChildren<PlayerMovementManager>().Init(movementSpeed);
        GetComponentInChildren<PlayerAttackManager>().Init(attackDelay);
    }
}
