using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] string playerName;
    [Range(1, 2)]
    [SerializeField] int index;
    [SerializeField] float movementSpeed;
    [SerializeField] float attackDelay;
    [SerializeField] int life;

    public static UnityAction<int, Bullet> OnShoot;
    public static UnityAction<int, Bullet> OnHit;
    public static UnityAction<int, int> OnLifeChange;
    public string PlayerName => playerName;
    public int Index => index;


    public void Awake()
    {
        GetComponentInChildren<PlayerMovementManager>().Init(index, movementSpeed);
        GetComponentInChildren<PlayerAnimations>().Init(index);
        GetComponentInChildren<PlayerAttackManager>().Init(index, attackDelay);
        GetComponentInChildren<OxygenRenderer>().Init(index);
    }

    public void Hit(Bullet bullet)
    {
        if (GameManager.instance.gamePaused)
            return;
            
        Debug.Log($"player took damage {bullet.Damage}");
        life -= bullet.Damage;

        OnHit?.Invoke(index, bullet);
        OnLifeChange?.Invoke(index, life);
        
        if (life <= 0)
        {
            RoundsManager.instance.SetRoundLoser(index);
        }
    }
}
