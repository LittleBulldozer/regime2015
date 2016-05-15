using UnityEngine;
using UnityEditor;
using System.Collections;

public class DebugHelper
{
    [MenuItem("Short Cuts/Pause _p")]
    static void Pause()
    {
        if (Application.isPlaying == true)
        {
            EditorApplication.isPaused = !EditorApplication.isPaused;
        }
    }
}
