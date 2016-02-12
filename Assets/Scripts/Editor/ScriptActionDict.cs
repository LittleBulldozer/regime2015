using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class ScriptActionDict : ScriptableObject
{
	public const string singletonPath = "Assets/Scripts/Editor/Resources/ScriptActionDict.asset";

	public static ScriptActionDict singleton
	{
		get
		{
			var asset = AssetDatabase.LoadAssetAtPath<ScriptActionDict>(singletonPath);
			if (asset == null)
			{
				asset = ScriptableObject.CreateInstance<ScriptActionDict>();
				AssetDatabase.CreateAsset(asset, singletonPath);
				EditorUtility.SetDirty(asset);
				AssetDatabase.SaveAssets();
			}

			asset.ClearNullItems();

			return asset;
		}
	}

	public List<ScriptAction> scriptActions = new List<ScriptAction>();

	void ClearNullItems()
	{
		scriptActions.RemoveAll(x => x == null);
	}

	public int GetNextId()
	{
		int id = 0;
		while (scriptActions.Any(x => x.id == id))
		{
			id++;
		}
		return id;
	}

}
