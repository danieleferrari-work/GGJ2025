using UnityEngine;

public class MovementCircle : MonoBehaviour
{
    [SerializeField] float radius;
    public float Radius => radius;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
