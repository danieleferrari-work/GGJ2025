using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int damage;
    [SerializeField] float lifeTime;
    [SerializeField] float equilibriumAlteration;

    public float EquilibriumAlteration => equilibriumAlteration;
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
