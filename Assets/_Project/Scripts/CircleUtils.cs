using UnityEngine;

public static class CircleUtils
{
    public static Vector3 GetPositionBasedOnAngle(Vector3 center, float radius, float angle)
    {
        float x = radius * Mathf.Cos(angle);
        float z = radius * Mathf.Sin(angle);
        var targetPosition = new Vector3(x, 0, z) + center;
        return targetPosition;
    }
}
