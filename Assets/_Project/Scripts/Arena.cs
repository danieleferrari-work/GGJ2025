using UnityEngine;

public class Arena : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] float radius;
    public float Radius => radius;


    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);

        // Converti equilibrium in un angolo in radianti
        float angle = gameManager.equilibrium * Mathf.PI; // Equilibrium va da 0 a 1, quindi moltiplichiamo per PI

        // Calcola la posizione del primo raggio
        float x1 = radius * Mathf.Cos(angle);
        float z1 = radius * Mathf.Sin(angle);
        var firstRadiusPosition = new Vector3(x1, 0, z1) + transform.position;

        // Calcola la posizione del secondo raggio (simmetrico al primo)
        float x2 = radius * Mathf.Cos(-angle);
        float z2 = radius * Mathf.Sin(-angle);
        var secondRadiusPosition = new Vector3(x2, 0, z2) + transform.position;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, firstRadiusPosition);
        Gizmos.DrawLine(transform.position, secondRadiusPosition);
    }
}
