using UnityEngine;

public class Arena : MonoBehaviour
{
    [SerializeField] float radius;
    [Range(0.1f, 0.9f)] public float equilibrium;
    public float equilibriumChangeSpeed;

    private float firstAngle;
    private float targetFirstAngle;
    private float targetSecondAngle;
    private float secondAngle;

    public float Radius => radius;
    public float FirstAngle => firstAngle;
    public float SecondAngle => secondAngle;


    void Start()
    {
        ResetEquilibrium();
        Player.OnShoot += AlterEquilibriumOnShoot;
    }

    public void ResetEquilibrium()
    {
        equilibrium = 0.5f;

        CalculateZonesAngles();
        UpdateZoneAngles();
    }

    void Update()
    {
        CalculateZonesAngles();
        // UpdateZoneAngles();
        UpdateZoneAnglesLerping();
    }

    void OnDestroy()
    {
        Player.OnHit -= AlterEquilibriumOnHit;
        Player.OnShoot -= AlterEquilibriumOnShoot;
    }

    public void AlterEquilibrium(int playerIndex, float value)
    {
        equilibrium = playerIndex == 1 ? equilibrium - value : equilibrium + value;
        equilibrium = Mathf.Clamp(equilibrium, 0.1f, 0.9f);
    }

    public void AlterEquilibriumOnHit(int playerIndex, Bullet bullet)
    {
        AlterEquilibrium(playerIndex, bullet.EquilibriumLostOnHit);
    }

    public void AlterEquilibriumOnShoot(int playerIndex, Bullet bullet)
    {
        AlterEquilibrium(playerIndex, bullet.EquilibriumLostOnShoot);
    }

    private void CalculateZonesAngles()
    {
        float angle = equilibrium * Mathf.PI; // Equilibrium va da 0 a 1, quindi moltiplichiamo per PI

        var firstRadiusPosition = CircleUtils.GetPositionBasedOnAngle(transform.position, radius, angle);
        var secondRadiusPosition = CircleUtils.GetPositionBasedOnAngle(transform.position, radius, -angle);

        // Calcola gli angoli dei raggi
        targetFirstAngle = Mathf.Atan2(firstRadiusPosition.z - transform.position.z, firstRadiusPosition.x - transform.position.x);
        targetSecondAngle = Mathf.Atan2(secondRadiusPosition.z - transform.position.z, secondRadiusPosition.x - transform.position.x);
    }

    private void UpdateZoneAngles()
    {
        firstAngle = targetFirstAngle;
        secondAngle = targetSecondAngle;
    }

    private void UpdateZoneAnglesLerping()
    {
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
}
