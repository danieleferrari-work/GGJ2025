using System;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{
    [SerializeField] Bullet bullet;
    [SerializeField] Transform bulletSpawnPoint;

    private InputManager inputManager;
    private float attackDelay;
    private float timeSinceLastAttack;

    public void Init(float delay)
    {
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

        if (inputManager.Player1Attack && timeSinceLastAttack >= attackDelay)
        {
            Debug.Log("Player1Attack");
            Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            timeSinceLastAttack = 0f;
        }
    }
}
