using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class ScriptConditionDict : AssetDict<ScriptCondition>
{
	public const string singletonPath = "Assets/Scripts/Editor/Resources/ScriptConditionDict.asset";

	public static ScriptConditionDict singleton
	{
		get
		{
			var asset = AssetDatabase.LoadAssetAtPath<ScriptConditionDict>(singletonPath);
			if (asset == null)
			{
				asset = ScriptableObject.CreateInstance<ScriptConditionDict>();
				AssetDatabase.CreateAsset(asset, singletonPath);
				EditorUtility.SetDirty(asset);
				AssetDatabase.SaveAssets();
			}

			asset.ClearNullItems();

			return asset;
		}
	}
}
