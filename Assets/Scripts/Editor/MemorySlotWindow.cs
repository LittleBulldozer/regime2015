using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Linq;

public class MemorySlotWindow : EditorWindow
{
	public const string singletonPath = "Assets/Resources/GameMemory.asset";

	[MenuItem("Tools/GameMemory")]
	public static void Open()
	{
		GetWindow<MemorySlotWindow>("GameMemory");
	}

	public void OnEnable()
	{
		memorySlot = AssetDatabase.LoadAssetAtPath<MemorySlot>(singletonPath);
		if (memorySlot == null)
		{
			memorySlot = ScriptableObject.CreateInstance<MemorySlot>();
			AssetDatabase.CreateAsset(memorySlot, singletonPath);
			EditorUtility.SetDirty(memorySlot);
			AssetDatabase.SaveAssets();
		}
	}

	public void OnGUI()
	{
		bool dirty = false;
		SlotDesc slotToDel = null;
		EditorGUI.BeginChangeCheck();
		foreach (var slot in memorySlot.slots)
		{
			GUILayout.BeginHorizontal();
			slot.dataType = (SlotDataType)EditorGUILayout.EnumPopup(slot.dataType, GUILayout.Width(70));
			if (slot.dataType == SlotDataType.CUSTOM)
			{
				slot.customDataType = EditorGUILayout.TextField(slot.customDataType);
			}
			GUILayout.Label(slot.name);

			GUILayout.Label("=");
			slot.initialValue = EditorGUILayout.TextField(slot.initialValue);

			if (GUILayout.Button("-"))
			{
				slotToDel = slot;
			}
			GUILayout.EndHorizontal();
		}
		dirty = dirty || EditorGUI.EndChangeCheck();

		if (slotToDel != null)
		{
			dirty = true;
			memorySlot.slots.Remove(slotToDel);
		}

		GUILayout.BeginHorizontal();
		newSlotName = EditorGUILayout.TextField("New Slot Name", newSlotName);
		if (GUILayout.Button("Add Slot"))
		{
			var newSlot = new SlotDesc();
			newSlot.name = newSlotName;
			if (memorySlot.slots.Exists((x) => x.name == newSlotName))
			{
				EditorUtility.DisplayDialog("Error", "Duplicated named slot found.", "OK");
			}
			else
			{
				memorySlot.slots.Add(newSlot);
				dirty = true;
				newSlotName = "";
			}
		}
		GUILayout.EndHorizontal();

		if (dirty)
		{
			Sync();
		}
	}

	MemorySlot memorySlot;
	string newSlotName;

	void Sync()
	{
		EditorUtility.SetDirty(memorySlot);
		AssetDatabase.SaveAssets();
		AssetDatabase.ImportAsset(singletonPath);
	}
}
