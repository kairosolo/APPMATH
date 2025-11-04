using UnityEngine;

public class CameraComponent : MonoBehaviour
{
    public float FocalLength;

    public float GetPerspectve(float ZDistance)
    {
        var focalSum = Mathf.Max(ZDistance + FocalLength, 0f);
        return focalSum > 0 ? FocalLength / focalSum : 0;
    }
}