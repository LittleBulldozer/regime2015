using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ScreenExtension : MonoBehaviour
{
    public delegate void SizeChangeEvent(int width, int height);
    public static event SizeChangeEvent OnSizeChange0;
    public static event SizeChangeEvent OnSizeChange1;

    public static void ChangeSize(int width, int height)
    {
        Screen.SetResolution(width, height, true);

        SizeChanged(width, height);
    }

    static void SizeChanged(int width, int height)
    {
        if (OnSizeChange0 != null)
        {
            OnSizeChange0(width, height);
        }

        if (OnSizeChange1 != null)
        {
            OnSizeChange1(width, height);
        }
    }

#if UNITY_EDITOR
    int cachedWidth;
    int cachedHeight;

    void CacheScreenSize()
    {
        cachedWidth = Screen.width;
        cachedHeight = Screen.height;
    }

    void Awake()
    {
        CacheScreenSize();
    }

    void Update()
    {
        if (cachedWidth != Screen.width
            || cachedHeight != Screen.height)
        {
            CacheScreenSize();

            SizeChanged(cachedWidth, cachedHeight);
        }
    }
#endif
}
