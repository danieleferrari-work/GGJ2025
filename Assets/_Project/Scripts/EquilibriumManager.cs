using System;
using UnityEngine;

public class EquilibriumManager : MonoBehaviour
{
    [Range(0.1f, 0.9f)]
    public float equilibrium;
    public float equilibriumChangeSpeed;

    void Start()
    {
        Player.OnShoot += AlterEquilibriumOnShoot;
    }

    void OnDestroy()
    {
        Player.OnHit -= AlterEquilibriumOnHit;
        Player.OnShoot -= AlterEquilibriumOnShoot;
    }

    public void AlterEquilibrium(int playerIndex, float value)
    {
        equilibrium = playerIndex == 1 ? equilibrium - value : equilibrium + value;
        equilibrium = Mathf.Clamp(equilibrium, 0.1f, 0.9f);
    }

    public void AlterEquilibriumOnHit(int playerIndex, Bullet bullet)
    {
        AlterEquilibrium(playerIndex, bullet.EquilibriumLostOnHit);
    }

    public void AlterEquilibriumOnShoot(int playerIndex, Bullet bullet)
    {
        AlterEquilibrium(playerIndex, bullet.EquilibriumLostOnShoot);
    }
}
