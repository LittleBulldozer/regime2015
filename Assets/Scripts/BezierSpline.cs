﻿using UnityEngine;
using System;

public class BezierSpline : MonoBehaviour {

	[SerializeField]
	private Vector3[] points;

    [SerializeField]
    private float[] ups;

    [SerializeField]
	private BezierControlPointMode[] modes;

	[SerializeField]
	private bool loop;

	public bool Loop {
		get {
			return loop;
		}
		set {
			loop = value;
			if (value == true) {
				modes[modes.Length - 1] = modes[0];
				SetControlPoint(0, points[0]);
			}
		}
	}

	public int ControlPointCount {
		get {
			return points.Length;
		}
	}

	public Vector3 GetControlPoint (int index) {
		return points[index];
	}

    public float GetUpAt(int index)
    {
        return ups[index];
    }

    public void SetControlPoint (int index, Vector3 point) {
		if (index % 3 == 0) {
			Vector3 delta = point - points[index];
			if (loop) {
				if (index == 0) {
					points[1] += delta;
					points[points.Length - 2] += delta;
					points[points.Length - 1] = point;
				}
				else if (index == points.Length - 1) {
					points[0] = point;
					points[1] += delta;
					points[index - 1] += delta;
				}
				else {
					points[index - 1] += delta;
					points[index + 1] += delta;
				}
			}
			else {
				if (index > 0) {
					points[index - 1] += delta;
				}
				if (index + 1 < points.Length) {
					points[index + 1] += delta;
				}
			}
		}
		points[index] = point;
		EnforceMode(index);
	}

    public void SetUp(int index, float up)
    {
        ups[index] = up;
    }

	public BezierControlPointMode GetControlPointMode (int index) {
		return modes[(index + 1) / 3];
	}

	public void SetControlPointMode (int index, BezierControlPointMode mode) {
		int modeIndex = (index + 1) / 3;
		modes[modeIndex] = mode;
		if (loop) {
			if (modeIndex == 0) {
				modes[modes.Length - 1] = mode;
			}
			else if (modeIndex == modes.Length - 1) {
				modes[0] = mode;
			}
		}
		EnforceMode(index);
	}

	private void EnforceMode (int index) {
		int modeIndex = (index + 1) / 3;
		BezierControlPointMode mode = modes[modeIndex];
		if (mode == BezierControlPointMode.Free || !loop && (modeIndex == 0 || modeIndex == modes.Length - 1)) {
			return;
		}

		int middleIndex = modeIndex * 3;
		int fixedIndex, enforcedIndex;
		if (index <= middleIndex) {
			fixedIndex = middleIndex - 1;
			if (fixedIndex < 0) {
				fixedIndex = points.Length - 2;
			}
			enforcedIndex = middleIndex + 1;
			if (enforcedIndex >= points.Length) {
				enforcedIndex = 1;
			}
		}
		else {
			fixedIndex = middleIndex + 1;
			if (fixedIndex >= points.Length) {
				fixedIndex = 1;
			}
			enforcedIndex = middleIndex - 1;
			if (enforcedIndex < 0) {
				enforcedIndex = points.Length - 2;
			}
		}

		Vector3 middle = points[middleIndex];
		Vector3 enforcedTangent = middle - points[fixedIndex];
		if (mode == BezierControlPointMode.Aligned) {
			enforcedTangent = enforcedTangent.normalized * Vector3.Distance(middle, points[enforcedIndex]);
		}
		points[enforcedIndex] = middle + enforcedTangent;
	}

	public int CurveCount {
		get {
			return (points.Length - 1) / 3;
		}
	}

	public Vector3 GetPoint (float t) {
		int i;
		if (t >= 1f) {
			t = 1f;
			i = points.Length - 4;
		}
		else {
			t = Mathf.Clamp01(t) * CurveCount;
			i = (int)t;
			t -= i;
			i *= 3;
		}
		return transform.TransformPoint(Bezier.GetPoint(points[i], points[i + 1], points[i + 2], points[i + 3], t));
	}
	
	public Vector3 GetVelocity (float t) {
		int i;
		if (t >= 1f) {
			t = 1f;
			i = points.Length - 4;
		}
		else {
			t = Mathf.Clamp01(t) * CurveCount;
			i = (int)t;
			t -= i;
			i *= 3;
		}
		return transform.TransformPoint(Bezier.GetFirstDerivative(points[i], points[i + 1], points[i + 2], points[i + 3], t)) - transform.position;
	}
	
	public Vector3 GetDirection (float t) {
		return GetVelocity(t).normalized;
	}

    public void LookForward(Transform T, float t)
    {
        var dir = GetDirection(t);
        var N = dir.normalized;
        var worldUpInN = Vector3.ProjectOnPlane(Vector3.up, N);
        var rot = Quaternion.AngleAxis(GetUp(t), N);
        T.LookAt(T.position + dir, rot * worldUpInN);
    }

    public float GetUp(float t)
    {
        int i = Mathf.FloorToInt(t * points.Length);
        if (i == points.Length) i--;
        var up = ups[i];
        int j = i + 1;
        if (j >= points.Length) j--;
        var upj = ups[j];
        var tt = t * points.Length - (float)i;
        return up * (1 - tt) + upj * (tt);
    }

	public void AddCurve () {
		Vector3 point = points[points.Length - 1];
		Array.Resize(ref points, points.Length + 3);
		point.x += 1f;
		points[points.Length - 3] = point;
		point.x += 1f;
		points[points.Length - 2] = point;
		point.x += 1f;
		points[points.Length - 1] = point;

		Array.Resize(ref modes, modes.Length + 1);
		modes[modes.Length - 1] = modes[modes.Length - 2];
		EnforceMode(points.Length - 4);

        Array.Resize(ref ups, ups.Length + 3);
        ups[ups.Length - 3] = 0f;
        ups[ups.Length - 2] = 0f;
        ups[ups.Length - 1] = 0f;

        if (loop) {
			points[points.Length - 1] = points[0];
			modes[modes.Length - 1] = modes[0];
			EnforceMode(0);
		}
	}
	
	public void Reset () {
		points = new Vector3[] {
			new Vector3(1f, 0f, 0f),
			new Vector3(2f, 0f, 0f),
			new Vector3(3f, 0f, 0f),
			new Vector3(4f, 0f, 0f)
		};
        ups = new float[] { 0, 0, 0, 0 };
        modes = new BezierControlPointMode[] {
			BezierControlPointMode.Free,
			BezierControlPointMode.Free
		};
	}
}