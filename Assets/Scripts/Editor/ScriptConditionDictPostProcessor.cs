using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Linq;
using System;

public class ScriptConditionDictPostProcessor : AssetPostprocessor
{
	const string genPath = "Assets/Scripts/Generated/ConditionScriptDict.cs";

	static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
	{
		var changed = importedAssets.Any(x => x == thePath)
			|| deletedAssets.Any(x => x == thePath)
			|| movedAssets.Any(x => x == thePath)
			|| movedFromAssetPaths.Any(x => x == thePath);
		if (changed)
		{
			Regenerate();
		}
	}

	static void Regenerate()
	{
		var dict = ScriptConditionDict.singleton;

		var tpl = Resources.Load<TextAsset>("Templates/ConditionScriptDict");
		var tplStr = tpl.ToString();

		var csScript = CSFactory.MakeFromTemplate(tplStr
			, new CSFactory.TokenHandler("Fields", () => {
				var str = "";
				foreach (var scriptAction in dict.items)
				{
					str += string.Format("static bool S_{0}(MemoryData memory)\n", scriptAction.id);
					str += "{\n";
					var splited = scriptAction.script.Split('\n');
					str += string.Join("\n", splited, 0, splited.Length - 1);
					str += string.Format("\nreturn {0};", splited[splited.Length - 1]);
					str += "}\n";
				}
				return str;
			})
			, new CSFactory.TokenHandler("Cases", () => {
				var str = "";
				foreach (var scriptAction in dict.items)
				{
					str += string.Format("case {0}:\n", scriptAction.id);
					str += string.Format("return S_{0}(memoryData);\n", scriptAction.id);
				}
				return str;
			}));

		var sw = new System.IO.StreamWriter(genPath);
		sw.Write(csScript);
		sw.Close();
	}

	static string thePath
	{
		get
		{
			return ScriptConditionDict.singletonPath;
		}
	}
}
