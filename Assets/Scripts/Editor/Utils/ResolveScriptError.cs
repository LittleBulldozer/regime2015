using UnityEngine;
using UnityEditor;
using System.Collections;

public class ResolveScriptError
{
    [MenuItem("Help/Resolve script error")]
    public static void Resolve()
    {
        ScriptActionDictPostProcessor.Regenerate(true);
        ScriptConditionDictPostProcessor.Regenerate(true);
    }

}
