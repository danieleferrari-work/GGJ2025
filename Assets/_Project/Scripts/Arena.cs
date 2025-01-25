using UnityEngine;

public class Arena : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] float equilibriumChangeSpeed;
    [SerializeField] float radius;
    public float Radius => radius;

    private float firstAngle;
    private float secondAngle;


    void Update()
    {
        float angle = gameManager.equilibrium * Mathf.PI; // Equilibrium va da 0 a 1, quindi moltiplichiamo per PI

        var firstRadiusPosition = CalculateFirstRadiusPosition(angle);
        var secondRadiusPosition = CalculateSecondRadiusPosition(angle);

        // Calcola gli angoli dei raggi
        float targetFirstAngle = Mathf.Atan2(firstRadiusPosition.z - transform.position.z, firstRadiusPosition.x - transform.position.x);
        float targetSecondAngle = Mathf.Atan2(secondRadiusPosition.z - transform.position.z, secondRadiusPosition.x - transform.position.x);

        // Aggiorna gli angoli gradualmente
        firstAngle = Mathf.LerpAngle(firstAngle, targetFirstAngle, equilibriumChangeSpeed * Time.deltaTime);
        secondAngle = Mathf.LerpAngle(secondAngle, targetSecondAngle, equilibriumChangeSpeed * Time.deltaTime);
    }

    public float ClampPlayerAngle(int playerIndex, float candidateAngle)
    {
        if (playerIndex == 1 && IsAngleBetween(candidateAngle, secondAngle, firstAngle))
        {
            return candidateAngle;
        }
        else if (playerIndex == 2 && IsAngleBetween(candidateAngle, firstAngle, secondAngle))
        {
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
        
        // Calcola le nuove posizioni basate sugli angoli
        Vector3 firstAnglePosition = CircleUtils.GetPositionBasedOnAngle(transform.position, radius, firstAngle);
        Vector3 secondAnglePosition =  CircleUtils.GetPositionBasedOnAngle(transform.position, radius, secondAngle);

        // Disegna i Gizmos utilizzando le nuove posizioni
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, firstAnglePosition);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, secondAnglePosition);
    }
}
