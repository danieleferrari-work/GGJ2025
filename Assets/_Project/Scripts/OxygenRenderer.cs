using UnityEngine;

public class OxygenRenderer : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer1;
    [SerializeField] LineRenderer lineRenderer2;

    [SerializeField] int density;

    [SerializeField] Arena arena;

    void Update()
    {
        lineRenderer1.positionCount = density + 1;
        for (int i = 0; i <= density; i++)
        {
            var rad = (float)i / density * ((arena.FirstAngle - arena.SecondAngle)/2);
            var position = new Vector3(Mathf.Cos(rad), 0, Mathf.Sin(rad)) * arena.Radius;
            lineRenderer1.SetPosition(i, position);
        }

        lineRenderer2.positionCount = density + 1;
        for (int i = 0; i <= density; i++)
        {
            var rad = (float)i / density * ((arena.FirstAngle - arena.SecondAngle) / 2);
            var position = new Vector3(Mathf.Cos(rad), 0, -Mathf.Sin(rad)) * arena.Radius;
            lineRenderer2.SetPosition(i, position);
        }
    }
}
