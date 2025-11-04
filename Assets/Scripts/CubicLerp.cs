using UnityEngine;

public class CubicLerp : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private Transform pointC;
    [SerializeField] private Transform pointD;
    [SerializeField] private float timeToEnd;
    private float timeStart;
    public bool useCubic;

    private void Start()
    {
    }

    private void Update()
    {
        timeStart += Time.deltaTime;
        if (useCubic)
        {
            this.transform.position = CubicCurve(pointA.position, pointB.position, pointC.position, pointD.position, Mathf.Clamp01(timeStart / timeToEnd));
        }
        else
        {
            this.transform.position = QuadraticCurve(pointA.position, pointB.position, pointC.position, Mathf.Clamp01(timeStart / timeToEnd));
        }
    }

    public Vector3 QuadraticCurve(Vector3 start, Vector3 control, Vector3 end, float time)
    {
        /*        var lerpA = Vector3.Lerp(start, control, time);
                var lerpB = Vector3.Lerp(cotnrol, end, time);
                var lerpC = Vector3.Lerp(lerpA, lerpB, time);*/

        return Mathf.Pow(1 - time, 2) * start + 2 * (1 - time) * control * time + time * time * end;
    }

    public Vector3 CubicCurve(Vector3 start, Vector3 controlA, Vector3 controlB, Vector3 end, float time)
    {
        return Mathf.Pow(1 - time, 3) * start
            + Mathf.Pow(1 - time, 2) * controlA * 3 * time
            + (1 - time) * controlB * time * time * 3
            + time * time * time * end;
    }

    [ContextMenu("Reset")]
    private void Reset()
    {
        timeStart = 0;
    }
}