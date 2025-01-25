using System.Collections;
using UnityEngine;

public class EquilibriumManager : MonoBehaviour
{
    [Range(0.1f, 0.9f)]
    public float equilibrium;
    [SerializeField] float equilibriumChangeOnHit;
    [SerializeField] float equilibriumRechargeSpeed;
    [SerializeField] float equilibriumRechargeDelay;

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
        AlterEquilibrium(playerIndex, equilibriumChangeOnHit);
    }

    public void AlterEquilibriumOnShoot(int playerIndex, Bullet bullet)
    {
        AlterEquilibrium(playerIndex, bullet.EquilibriumAlteration);
    }

    private IEnumerator EquilibrateEquilibrium()
    {
        while (true)
        {
            equilibrium = Mathf.Lerp(equilibrium, 0.5f, equilibriumRechargeSpeed);
            yield return new WaitForSeconds(equilibriumRechargeDelay);
        }
    }
}
