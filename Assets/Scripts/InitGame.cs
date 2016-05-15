using UnityEngine;
using System.Collections;

public class InitGame : MonoBehaviour
{
    public int[] heights = new int[]
    {
        640
        , 960
        , 1280
        , 1366
        , 1600
        , 1920
    };

    public int minimumDpi = 250;

    void Awake ()
    {
        float device_dpi = (float)Screen.dpi;
        Debug.Log("Device dpi : " + Screen.dpi);
        Debug.Log("Device size : " + Screen.width + " x " + Screen.height);

        float R = (float)Screen.width / Screen.height;
        if (device_dpi < minimumDpi)
        {
            Debug.Log(string.Format("This device cannot acquire minimum dpi. (Device dpi : {0}) "
                , device_dpi));
            return;
        }

        for (int i = 0; i < heights.Length; i++)
        {
            int height = heights[i];
            int width = Mathf.FloorToInt(height * R);

            float ratio = (float)width / Screen.width;
            ratio = Mathf.Pow(ratio, 2.0f);
            if (ratio > 1.0f)
            {
                Debug.Log("ratio > 1.0f !!!");
                break;
            }

            float virtual_dpi = device_dpi * ratio;
            if (i == heights.Length - 1 || virtual_dpi >= minimumDpi)
            {
                Debug.Log("Set to " + width + " x " + height + " with virtual dpi : " + virtual_dpi);
                ScreenExtension.ChangeSize(width, height);
                break;
            }
        }
    }
}
