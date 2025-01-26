using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{
    [SerializeField] Bullet bullet;
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] ParticleSystem muzzleFlash;

    private InputManager inputManager;
    private GameManager gameManager;
    private int playerIndex;
    private float attackDelay;
    [SerializeField] private float attackAcceleration;
    private float timeSinceLastAttack;

    public void Init(int playerIndex, float delay)
    {
        this.playerIndex = playerIndex;
        this.attackDelay = delay;
        this.timeSinceLastAttack = delay;
    }

    void Awake()
    {
        inputManager = FindFirstObjectByType<InputManager>();
        gameManager = FindFirstObjectByType<GameManager>();
    }

    void Update()
    {
        timeSinceLastAttack += Time.deltaTime;

        if (inputManager.GetPlayerAttack(playerIndex) && timeSinceLastAttack >= attackDelay)
        {
            Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            timeSinceLastAttack = 0f;
            Player.OnShoot?.Invoke(playerIndex, bullet);
            muzzleFlash.Play();
        }
    }
}
