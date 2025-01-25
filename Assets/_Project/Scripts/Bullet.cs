using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float lifeTime;


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
