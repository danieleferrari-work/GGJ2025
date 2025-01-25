using UnityEngine;

public class Arena : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] float radius;
    public float Radius => radius;

    private Vector3 firstRadiusPosition;
    private Vector3 secondRadiusPosition;
    private float firstAngle;
    private float secondAngle;

    void Update()
    {
        UpdateRadiusPositions();
        UpdateAngles();
    }

    private void UpdateRadiusPositions()
    {
        float angle = gameManager.equilibrium * Mathf.PI; // Equilibrium va da 0 a 1, quindi moltiplichiamo per PI
        firstRadiusPosition = CalculateRadiusPosition(angle);
        secondRadiusPosition = CalculateRadiusPosition(angle + Mathf.PI);
    }

    private void UpdateAngles()
    {
        firstAngle = CalculateAngle(firstRadiusPosition);
        secondAngle = CalculateAngle(secondRadiusPosition);
    }

    private Vector3 CalculateRadiusPosition(float angle)
    {
        float x = radius * Mathf.Cos(angle);
        float z = radius * Mathf.Sin(angle);
        return new Vector3(x, 0, z) + transform.position;
    }

    private float CalculateAngle(Vector3 position)
    {
        return Mathf.Atan2(position.z - transform.position.z, position.x - transform.position.x);
    }

    public float ClampPlayerAngle(int playerIndex, float candidateAngle)
    {
        // Normalizza gli angoli
        candidateAngle = NormalizeAngle(candidateAngle);
        firstAngle = NormalizeAngle(firstAngle);
        secondAngle = NormalizeAngle(secondAngle);

        if (playerIndex == 1 && IsAngleBetween(candidateAngle, secondAngle, firstAngle))
        {
            return candidateAngle;
        }
        else if (playerIndex == 2 && IsAngleBetween(candidateAngle, firstAngle, secondAngle))
        {
            return candidateAngle;
        }

        // Se non è compreso, ritorna l'angolo del raggio più vicino
        return GetClosestAngle(candidateAngle);
    }

    private float NormalizeAngle(float angle)
    {
        angle %= 2 * Mathf.PI;
        if (angle < 0) angle += 2 * Mathf.PI;
        return angle;
    }

    private bool IsAngleBetween(float angle, float start, float end)
    {
        if (start < end)
            return start <= angle && angle <= end;
        return start <= angle || angle <= end;
    }

    private float GetClosestAngle(float candidateAngle)
    {
        float distanceToFirst = Mathf.Abs(Mathf.DeltaAngle(candidateAngle * Mathf.Rad2Deg, firstAngle * Mathf.Rad2Deg));
        float distanceToSecond = Mathf.Abs(Mathf.DeltaAngle(candidateAngle * Mathf.Rad2Deg, secondAngle * Mathf.Rad2Deg));

        return distanceToFirst < distanceToSecond ? firstAngle : secondAngle;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);

        UpdateRadiusPositions();

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, firstRadiusPosition);
        Gizmos.DrawLine(transform.position, secondRadiusPosition);
    }
}