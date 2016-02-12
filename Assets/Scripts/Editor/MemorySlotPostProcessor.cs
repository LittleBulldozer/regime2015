using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Linq;
using System;

public class MemorySlotPostProcessor : AssetPostprocessor
{
	const string genPath = "Assets/Scripts/Generated/MemoryData.cs";

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
		var m = AssetDatabase.LoadAssetAtPath<MemorySlot>(thePath);
		if (m == null)
		{
			return;
		}

		var memoryDataTpl = Resources.Load<TextAsset>("Templates/MemoryData");
		var memoryDataTplStr = memoryDataTpl.ToString();

		var memoryDataCSScript = CSFactory.MakeFromTemplate(memoryDataTplStr
			, new CSFactory.TokenHandler("Fields", () => {
				var str = "";
				foreach (var slot in m.slots)
				{
					str += "[SerializeField]\n";
					str += string.Format("public {0} {1} = {2};\n"
						, slot.TypeString
						, slot.name
						, slot.initialValue);
				}
				return str;
			}));

		var sw = new System.IO.StreamWriter(genPath);
		sw.Write(memoryDataCSScript);
		sw.Close();
	}

	static string thePath
	{
		get
		{
			return MemorySlotWindow.singletonPath;
		}
	}
}
