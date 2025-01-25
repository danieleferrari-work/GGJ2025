using UnityEngine;

public class Player : MonoBehaviour
{
    [Range(1, 2)]
    [SerializeField] int index;
    [SerializeField] float movementSpeed;
    [SerializeField] float attackDelay;

    public void Awake()
    {
        GetComponentInChildren<PlayerMovementManager>().Init(index, movementSpeed);
        GetComponentInChildren<PlayerAttackManager>().Init(index, attackDelay);
    }
}
