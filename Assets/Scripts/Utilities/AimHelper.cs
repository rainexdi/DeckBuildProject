using UnityEngine;

public static class AimHelper
{
    public static void AimAtTarget (Transform aimTransform, Vector3 targetPos)
    {
        Vector3 aimDirection = (targetPos - aimTransform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        aimTransform.eulerAngles = new Vector3(0, 0, angle);
    }

    public static Vector3 GetAimDirection (Vector3 fromPos, Vector3 toPos)
    {
        return (toPos - fromPos).normalized;
    }

}
