using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BezierSpline))]
public class MoveLastBezierPoint : MonoBehaviour
{
    public float mm = 0.1f;

    void Awake()
    {
        var spline = GetComponent<BezierSpline>();
        var lastOne = spline.GetControlPoint(spline.ControlPointCount - 1);

        var mmm = mm * Random.insideUnitSphere;
        lastOne.x += mmm.x;
        lastOne.y += mmm.y;
        lastOne.z += mmm.z;
        spline.SetControlPoint(spline.ControlPointCount - 1, lastOne);
    }
}
