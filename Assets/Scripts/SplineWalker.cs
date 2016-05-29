﻿using UnityEngine;

[ExecuteInEditMode]
public class SplineWalker : MonoBehaviour {

    public BezierSpline spline;

    public float duration;

    public bool lookForward;

    public SplineWalkerMode mode;

    [System.NonSerialized]
    public bool simulateInEditMode = false;

    public void SetProgress(float progress)
    {
        this.progress = progress;
    }

    private float progress;
    private bool goingForward = true;

    private void Update()
    {
        if (Application.isPlaying == false && simulateInEditMode == false)
        {
            return;
        }

        if (goingForward)
        {
            progress += Time.deltaTime / duration;
            if (progress > 1f) {
                if (mode == SplineWalkerMode.Once) {
                    progress = 1f;
                }
                else if (mode == SplineWalkerMode.Loop) {
                    progress -= 1f;
                }
                else {
                    progress = 2f - progress;
                    goingForward = false;
                }
            }
        }
        else {
            progress -= Time.deltaTime / duration;
            if (progress < 0f) {
                progress = -progress;
                goingForward = true;
            }
        }

        Vector3 position = spline.GetPoint(progress);
        transform.position = position;
        if (lookForward)
        {
            spline.LookForward(transform, progress);
        }
    }
}