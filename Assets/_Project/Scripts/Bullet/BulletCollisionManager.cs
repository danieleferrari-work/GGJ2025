using UnityEngine;

public class BulletCollisionManager : MonoBehaviour
{
    Bullet bullet;
    [SerializeField] float spawnTime;
    
    void Awake()
    {
        bullet = GetComponentInParent<Bullet>();
    }

    private void Update()
    {
        if(spawnTime > 0)
            spawnTime -= Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (spawnTime > 0)
            return;

        PlayerCollisionManager playerCollisionManager = other.GetComponent<PlayerCollisionManager>();
        if (playerCollisionManager != null)
        {
            playerCollisionManager.OnBulletCollision(bullet);
            bullet.StopAllCoroutines();
            Destroy(bullet.gameObject);
        }
    }
}
