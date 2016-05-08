using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Linq;
using System;

public class ScriptActionDictPostProcessor : AssetPostprocessor
{
	const string genPath = "Assets/Scripts/Generated/ActionScriptDict.cs";

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
		var dict = ScriptActionDict.singleton;

		var tpl = Resources.Load<TextAsset>("Templates/ActionScriptDict");
		var tplStr = tpl.ToString();

		var csScript = CSFactory.MakeFromTemplate(tplStr
			, new CSFactory.TokenHandler("Fields", () => {
				var str = "";
				foreach (var scriptAction in dict.items)
				{
					str += string.Format("static void S_{0}(int turn, MemoryData memory)\n", scriptAction.id);
					str += "{\n";
					str += scriptAction.script;
					str += "}\n";
				}
				return str;
			})
			, new CSFactory.TokenHandler("Cases", () => {
				var str = "";
				foreach (var scriptAction in dict.items)
				{
					str += string.Format("case {0}:\n", scriptAction.id);
					str += string.Format("S_{0}(nrTurn, memoryData);\n", scriptAction.id);
					str += "break;\n";
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
			return ScriptActionDict.singletonPath;
		}
	}
}
