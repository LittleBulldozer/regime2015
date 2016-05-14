using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class IAmTheCamera : MonoBehaviour
{
    public Camera cam;

    void OnEnable()
    {
        Reposition();

        ScreenExtension.OnSizeChange1 += OnScreenSizeChange;
    }

    void OnDisable()
    {
        ScreenExtension.OnSizeChange1 -= OnScreenSizeChange;
    }

    void Reposition()
    {
        Debug.Log("Reposition!");

        cam.transform.position = transform.position - 1 * transform.forward;
        cam.transform.LookAt(transform);
        cam.orthographic = true;

        var size = transform.GetComponent<BoxCollider>().size;
        var LS = transform.localScale;
        size.x *= LS.x;
        size.y *= LS.y;
        cam.orthographicSize = 0.5f * size.y;
    }

    void OnScreenSizeChange(int width, int height)
    {
        Reposition();
    }
}
