using UnityEngine;

public class OxygenRenderer : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer1;
    [SerializeField] LineRenderer lineRenderer2;

    [SerializeField] int density;

    [SerializeField] Arena arena;

    int index;
    [SerializeField] float yPos;

    public void Init(int index)
    {
        this.index = index;
    }

    void Update()
    {
        lineRenderer1.positionCount = density + 1;
        for (int i = 0; i <= density; i++)
        {
            Vector3 position = Vector3.zero;
            if (index == 1)
            {
                var rad = (float)i / density * ((arena.FirstAngle - arena.SecondAngle) / 2);
                position = new Vector3(Mathf.Cos(rad), yPos, Mathf.Sin(rad)) * arena.Radius;
            }

            if (index == 2)
            {
                var rad = (float)i / density * ((Mathf.Deg2Rad * 360 - (arena.FirstAngle - arena.SecondAngle)) / 2);
                position = new Vector3(-Mathf.Cos(rad), yPos, Mathf.Sin(rad)) * arena.Radius;
            }

            lineRenderer1.SetPosition(i, position);
        }

        lineRenderer2.positionCount = density + 1;
        for (int i = 0; i <= density; i++)
        {
            Vector3 position = Vector3.zero;
            if (index == 1)
            {
                var rad = (float)i / density * ((arena.FirstAngle - arena.SecondAngle) / 2);
                position = new Vector3(Mathf.Cos(rad), yPos, -Mathf.Sin(rad)) * arena.Radius;
            }

            if (index == 2)
            {
                var rad = (float)i / density * ((Mathf.Deg2Rad * 360 - (arena.FirstAngle - arena.SecondAngle)) / 2);
                position = new Vector3(-Mathf.Cos(rad), yPos, -Mathf.Sin(rad)) * arena.Radius;
            }
            lineRenderer2.SetPosition(i, position);
        }
    }
}
