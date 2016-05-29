using UnityEngine;
using UnityEngine.Events;

[ExecuteInEditMode]
public class SplineWalker : MonoBehaviour {

    public BezierSpline spline;

    public float duration = 1f;

    public bool lookForward;

    public SplineWalkerMode mode;

    public AnimationCurve easeCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

    public bool simulateInEditMode = false;

    public UnityEvent onEnd = new UnityEvent();
    
    [SerializeField]
    [Range(0f, 1f)]
    private float progress = 0f;
    private bool goingForward = true;

    private void Start()
    {
        progress = 0f;
    }

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
                    enabled = false;
                    onEnd.Invoke();
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

        var normalizedProg = easeCurve.Evaluate(progress);

        Vector3 position = spline.GetPoint(normalizedProg);
        transform.position = position;
        if (lookForward)
        {
            spline.LookForward(transform, normalizedProg);
        }
    }
}