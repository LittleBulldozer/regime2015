using UnityEngine;
using System.Collections;

public class TheWorld : MonoBehaviour
{
	public static TheWorld instance;

	public static WorldTimer timer;

	public static WorldNoteRef noteRef;

	public static Memory memory;

	public static CardPool cardPool;

	void Awake()
	{
		instance = this;
		timer = GetComponent<WorldTimer>();
		noteRef = GetComponent<WorldNoteRef>();
		memory = GetComponent<Memory>();
		cardPool = GetComponent<CardPool>();
	}
}
