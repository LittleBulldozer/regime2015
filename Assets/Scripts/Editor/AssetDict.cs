using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public abstract class AssetDict<T> : ScriptableObject where T : IUniqueID
{
	public List<T> items = new List<T>();

	public int GetNextId()
	{
		int id = 0;
		while (items.Any(x => x.GetID() == id))
		{
			id++;
		}
		return id;
	}

	protected void ClearNullItems()
	{
		items.RemoveAll(x => x == null);
	}
}
