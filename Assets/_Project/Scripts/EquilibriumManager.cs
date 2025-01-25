using System.Collections;
using UnityEngine;

public class EquilibriumManager : MonoBehaviour
{
    [Range(0.1f, 0.9f)]
    public float equilibrium;
    
    [Tooltip("Ogni quanti secondi si ricarica l'equilibrio")]
    [SerializeField] float equilibriumRechargeDelay;

    [Tooltip("Quanto equilibrio viene ristabilito ogni equilibriumRechargeDelay secondi")]
    [SerializeField] float equilibriumRechargeTick;

    public float equilibriumChangeSpeed;

    void Start()
    {
        StartCoroutine(EquilibrateEquilibrium());
        Player.OnHit += AlterEquilibriumOnHit;
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

    private IEnumerator EquilibrateEquilibrium()
    {
        while (true)
        {
            if (equilibrium - 0.5f > 0.03f)
            {
                if (equilibrium < 0.5f)
                {
                    equilibrium += equilibriumRechargeTick;
                }
                else
                {
                    equilibrium -= equilibriumRechargeTick;
                }
            }
            yield return new WaitForSeconds(equilibriumRechargeDelay);
        }
    }
}
