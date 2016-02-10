using System;
using UnityEngine;
using System.Collections.Generic;

public enum SlotDataType
{
	INT
	, FLOAT
	, STRING
	, BOOLEAN
	, CUSTOM
}

[Serializable]
public class SlotDesc
{
	public string name;
	public SlotDataType dataType;
	public string customDataType;
	public string initialValue;

	public string TypeString
	{
		get
		{
			if (dataType == SlotDataType.CUSTOM)
			{
				return customDataType;
			}
			else
			{
				return dataType.ToString().ToLower();
			}
		}
	}
}

public class MemorySlot : ScriptableObject
{
	public List<SlotDesc> slots = new List<SlotDesc>();
}