using System.Collections;
using UnityEngine;

public class EquilibriumManager : MonoBehaviour
{
    [Range(0.1f, 0.9f)]
    public float equilibrium;
    [SerializeField] float equilibriumAlterationOnHit;
    [SerializeField] float equilibriumRecharcgeTick;
    [SerializeField] float equilibriumRechargeDelay;
    public float equilibriumChangeSpeed;

    void Start()
    {
        StartCoroutine(EquilibrateEquilibrium());
        Player.OnLifeChange += AlterEquilibriumOnHit;
        Player.OnShoot += AlterEquilibriumOnShoot;
    }

    void OnDestroy()
    {
        Player.OnLifeChange -= AlterEquilibriumOnHit;
        Player.OnShoot -= AlterEquilibriumOnShoot;
    }

    public void AlterEquilibrium(int playerIndex, float value)
    {
        equilibrium = playerIndex == 1 ? equilibrium - value : equilibrium + value;
        equilibrium = Mathf.Clamp(equilibrium, 0.1f, 0.9f);
    }

    public void AlterEquilibriumOnHit(int playerIndex, int life)
    {
        AlterEquilibrium(playerIndex, equilibriumAlterationOnHit);
    }

    public void AlterEquilibriumOnShoot(int playerIndex, Bullet bullet)
    {
        AlterEquilibrium(playerIndex, bullet.EquilibriumAlteration);
    }

    private IEnumerator EquilibrateEquilibrium()
    {
        while (true)
        {
            if (equilibrium - 0.5f > 0.03f)
            {
                if (equilibrium < 0.5f)
                {
                    equilibrium += equilibriumRecharcgeTick;
                }
                else
                {
                    equilibrium -= equilibriumRecharcgeTick;
                }
            }
            yield return new WaitForSeconds(equilibriumRechargeDelay);
        }
    }
}
