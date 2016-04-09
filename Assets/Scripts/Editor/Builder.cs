using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using System.Linq;
using UnityEditor.SceneManagement;

public static class AndroidSDKFolder
{
    public static string Path
    {
        get { return EditorPrefs.GetString("AndroidSdkRoot"); }
        set { EditorPrefs.SetString("AndroidSdkRoot", value); }
    }
}

public class Builder
{
    public static string ApkDirPath
    {
        get { return EditorPrefs.GetString("ApkPath"); }
        set { EditorPrefs.SetString("ApkPath", value); }
    }

    [MenuItem("Build/Reset APK Path")]
    public static void ResetApkPath()
    {
        var R = EditorUtility.OpenFolderPanel("APK 저장 위치", "", "");
        if (R.Length > 0)
        {
            ApkDirPath = R;
        }
    }

    [MenuItem("Build/Make APK")]
    public static void MakeAPK()
    {
        if (ApkDirPath.Length == 0)
        {
            ResetApkPath();
        }

        if (ApkDirPath.Length == 0)
        {
            return;
        }
/*
        if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android)
        {
            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildTarget.Android);
        }
        */
        var path = string.Format("{0}/{1}.{2}.apk"
            , ApkDirPath
            , "regime"
            , GetTimeTag()
            );

        BuildPipeline.BuildPlayer(
            EditorBuildSettings.scenes.Select(s => s.path).ToArray()
            , path
            , BuildTarget.Android
            , BuildOptions.None
            );
    }

    [MenuItem("Build/Show Now Time Tag")]
    public static void ShowNowTimeTag()
    {
        var tag = GetTimeTag();
        EditorUtility.DisplayDialog(tag, tag, tag);
    }

    static string GetTimeTag()
    {
        var nowaday = DateTime.Today;
        var nowtime = DateTime.Now;

        return string.Format("{0:0000}-{1:00}-{2:00}.{3:00}{4:00}{5:00}"
            , nowaday.Year
            , nowaday.Month
            , nowaday.Day
            , nowtime.Hour
            , nowtime.Minute
            , nowtime.Second);
    }
}
