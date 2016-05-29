using UnityEngine;

[ExecuteInEditMode]
public class SplineWalker : MonoBehaviour {

    public BezierSpline spline;

    public float duration;

    public bool lookForward;

    public SplineWalkerMode mode;

    public void SetProgress(float prog)
    {
        Vector3 position = spline.GetPoint(prog);
        transform.position = position;
        if (lookForward)
        {
            transform.LookAt(position + spline.GetDirection(prog), spline.GetUp(prog));
        }
    }

    private float progress;
    private bool goingForward = true;

    private void Update()
    {
        if (Application.isPlaying == false)
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

        SetProgress(progress);
    }
}