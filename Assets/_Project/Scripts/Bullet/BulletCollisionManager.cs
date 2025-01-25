using UnityEngine;

public class BulletCollisionManager : MonoBehaviour
{
    Bullet bullet;
    
    void Awake()
    {
        bullet = GetComponentInParent<Bullet>();
    }
    
    void OnTriggerEnter(Collider other)
    {
        PlayerCollisionManager playerCollisionManager = other.GetComponent<PlayerCollisionManager>();
        if (playerCollisionManager != null)
        {
            playerCollisionManager.OnBulletCollision(bullet.Damage);
            bullet.StopAllCoroutines();
            Destroy(bullet.gameObject);
        }
    }
}
