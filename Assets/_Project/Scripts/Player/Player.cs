using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [Range(1, 2)]
    [SerializeField] int index;
    [SerializeField] float movementSpeed;
    [SerializeField] float attackDelay;
    [SerializeField] int life;

    public static UnityAction<int, int> OnLifeChange;


    public void Awake()
    {
        GetComponentInChildren<PlayerMovementManager>().Init(index, movementSpeed);
        GetComponentInChildren<PlayerAttackManager>().Init(index, attackDelay);
    }

    public void TakeDamage(int damage)
    {
        Debug.Log($"player took damage {damage}");
        life -= damage;

        OnLifeChange?.Invoke(index, life);
        
        if (life <= 0)
        {
            Destroy(gameObject);
            // TODO Game Over
        }
    }
}
