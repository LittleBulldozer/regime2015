using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class PlaneRenderer : MonoBehaviour
{
    public Renderer plane;

    RenderTexture RT = null;
    Camera cam = null;

    void Awake()
    {
        cam = GetComponent<Camera>();
    }

    void OnEnable()
    {
        MakeRenderTarget();
        ScreenExtension.OnSizeChange0 += OnScreenSizeChange;
    }

    void OnDisable()
    {
        ScreenExtension.OnSizeChange0 -= OnScreenSizeChange;
        ReleaseRenderTarget();
    }

    void MakeRenderTarget()
    {
        Debug.Log("[PlaneRenderer] MakeRenderTarget");

        RT = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.Default);
        cam.targetTexture = RT;
        if (plane != null)
        {
            (
                Application.isPlaying
                ? plane.material
                : plane.sharedMaterial
            )
            .SetTexture("_MainTex", RT);

            var LS = plane.transform.localScale;
            LS.y = LS.x * Screen.height / Screen.width;
            plane.transform.localScale = LS;
        }
    }

    void ReleaseRenderTarget()
    {
        cam.targetTexture = null;
        RT.Release();
        RT = null;
    }

    void OnScreenSizeChange(int width, int height)
    {
        ReleaseRenderTarget();
        MakeRenderTarget();
    }

#if UNITY_EDITOR
    void Update()
    {
        if (Application.isPlaying == false)
        {
            if (RT == null)
            {
                MakeRenderTarget();
            }

            if (cam.targetTexture != RT)
            {
                cam.targetTexture = RT;
            }

            if (plane != null)
            {
                var mat = (
                    Application.isPlaying
                    ? plane.material
                    : plane.sharedMaterial
                );

                if (mat.GetTexture("_MainTex") != RT)
                {
                    mat.SetTexture("_MainTex", RT);
                }
            }
        }
    }
#endif
}
