using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int damage;
    [SerializeField] float lifeTime;

    [Tooltip("Quanto equilibrio perde il giocatore che viene colpito")]
    [SerializeField] float equilibriumLostOnHit;
    [Tooltip("Quanto equilibrio perde il giocatore che spara")]
    [SerializeField] float equilibriumLostOnShoot;

    public float EquilibriumLostOnHit => equilibriumLostOnHit;
    public float EquilibriumLostOnShoot => equilibriumLostOnShoot;

    public int Damage => damage;

    void Awake()
    {
        GetComponentInChildren<BulletMovementManager>().Init(speed);

        StartCoroutine(LifeTimeCoroutine());
    }

    public IEnumerator LifeTimeCoroutine()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
