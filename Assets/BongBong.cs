using UnityEngine;
using System.Collections;

public class BongBong : MonoBehaviour
{
    public float period = 2f;
    public float amplitude = 0.1f;

    public Rotate.XYZ xyz;

    float time = 0f;
    
    float lastValue;

    void Start ()
    {
        lastValue = GetValue();
    }

    void Update()
    {
        time += Time.deltaTime;
        var value = GetValue();
        var dv = value - lastValue;
        lastValue = value;
        Vector3 dvv = Vector3.zero;

        switch (xyz)
        {
            case Rotate.XYZ.X:
                dvv.x = dv;
                break;

            case Rotate.XYZ.Y:
                dvv.y = dv;
                break;

            case Rotate.XYZ.Z:
                dvv.z = dv;
                break;
        }

        transform.localPosition += transform.localRotation * dvv;
    }

    float GetValue()
    {
        return amplitude * Mathf.Sin(time / period);
    }
}
