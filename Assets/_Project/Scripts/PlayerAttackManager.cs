using System;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{
    [SerializeField] Bullet bullet;
    [SerializeField] Transform bulletSpawnPoint;

    private InputManager inputManager;
    private int playerIndex;
    private float attackDelay;
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
    }

    void Update()
    {
        timeSinceLastAttack += Time.deltaTime;

        if (inputManager.GetPlayerAttack(playerIndex) && timeSinceLastAttack >= attackDelay)
        {
            Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            timeSinceLastAttack = 0f;
        }
    }
}
