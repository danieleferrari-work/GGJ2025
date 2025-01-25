using UnityEngine;

public class Player : MonoBehaviour
{
    [Range(1, 2)]
    [SerializeField] int index;
    [SerializeField] float movementSpeed;
    [SerializeField] float attackDelay;
    [SerializeField] int life;


    public void Awake()
    {
        GetComponentInChildren<PlayerMovementManager>().Init(index, movementSpeed);
        GetComponentInChildren<PlayerAttackManager>().Init(index, attackDelay);
    }

    public void TakeDamage(int damage)
    {
        Debug.Log($"player took damage {damage}");
        life -= damage;
        if (life <= 0)
        {
            Destroy(gameObject);
            // TODO Game Over
        }
    }
}
