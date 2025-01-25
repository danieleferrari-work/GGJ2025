using UnityEngine;

public class Arena : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] float radius;
    public float Radius => radius;

    private Vector3 firstRadiusPosition;
    private float firstAngle;
    private Vector3 secondRadiusPosition;
    private float secondAngle;


    void Update()
    {
        float angle = gameManager.equilibrium * Mathf.PI; // Equilibrium va da 0 a 1, quindi moltiplichiamo per PI

        firstRadiusPosition = CalculateFirstRadiusPosition(angle);
        secondRadiusPosition = CalculateSecondRadiusPosition(angle);

        // Calcola gli angoli dei raggi
        firstAngle = Mathf.Atan2(firstRadiusPosition.z - transform.position.z, firstRadiusPosition.x - transform.position.x);
        secondAngle = Mathf.Atan2(secondRadiusPosition.z - transform.position.z, secondRadiusPosition.x - transform.position.x);
    }

    public float ClampPlayerAngle(int playerIndex, float candidateAngle)
    {
        Debug.Log($"playerIndex = {playerIndex}, candidateAngle = {candidateAngle}");

        if (playerIndex == 1 && IsAngleBetween(candidateAngle, secondAngle, firstAngle))
        {
            Debug.LogFormat("player 1 angle is ok={0}", candidateAngle);
            return candidateAngle;
        }
        else if (playerIndex == 2 && IsAngleBetween(candidateAngle, firstAngle, secondAngle))
        {
            Debug.LogFormat("player 2 angle is ok={0}", candidateAngle);
            return candidateAngle;
        }

        // Se non è compreso, ritorna l'angolo del raggio più vicino
        float distanceToFirst = Mathf.Abs(Mathf.DeltaAngle(candidateAngle * Mathf.Rad2Deg, firstAngle * Mathf.Rad2Deg));
        float distanceToSecond = Mathf.Abs(Mathf.DeltaAngle(candidateAngle * Mathf.Rad2Deg, secondAngle * Mathf.Rad2Deg));

        if (distanceToFirst < distanceToSecond)
        {
            return firstAngle;
        }
        else
        {
            return secondAngle;
        }
    }


    private float NormalizeAngle(float angle)
    {
        while (angle < 0) angle += 2 * Mathf.PI;
        while (angle > 2 * Mathf.PI) angle -= 2 * Mathf.PI;
        return angle;
    }

    private bool IsAngleBetween(float angle, float start, float end)
    {
        // Normalizza gli angoli tra 0 e 2*PI
        angle = NormalizeAngle(angle);
        start = NormalizeAngle(start);
        end = NormalizeAngle(end);

        if (start < end)
            return start <= angle && angle <= end;
        return start <= angle || angle <= end;
    }


    private Vector3 CalculateSecondRadiusPosition(float angle)
    {
        float x = radius * Mathf.Cos(-angle);
        float z = radius * Mathf.Sin(-angle);
        var secondRadiusPosition = new Vector3(x, 0, z) + transform.position;
        return secondRadiusPosition;
    }

    private Vector3 CalculateFirstRadiusPosition(float angle)
    {
        float x = radius * Mathf.Cos(angle);
        float z = radius * Mathf.Sin(angle);
        var firstRadiusPosition = new Vector3(x, 0, z) + transform.position;
        return firstRadiusPosition;
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, firstRadiusPosition);
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, secondRadiusPosition);
    }
}
