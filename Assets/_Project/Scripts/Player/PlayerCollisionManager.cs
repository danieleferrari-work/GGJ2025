using UnityEngine;

public class PlayerCollisionManager : MonoBehaviour
{
    Player player;

    void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    internal void OnBulletCollision(Bullet bullet)
    {
        player.Hit(bullet);
    }
}
