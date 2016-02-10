using UnityEngine;
using System.Collections;

public class Memory : MonoBehaviour
{
	public static Memory singleton
	{
		get
		{
			return _singleton;
		}
	}

	[System.NonSerialized]
	public MemoryData data = new MemoryData();

	static Memory _singleton;

	void Awake()
	{
		_singleton = this;
	}
}
