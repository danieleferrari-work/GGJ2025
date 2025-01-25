using UnityEngine;

public class BulletMovementManager : MonoBehaviour
{
    [SerializeField] Transform bulletTransform;
    private float speed;


    internal void Init(float speed)
    {
        this.speed = speed;
    }

    void Update()
    {
        bulletTransform.position += speed * Time.deltaTime * bulletTransform.forward;
    }
}
