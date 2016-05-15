using UnityEngine;
using System.Collections;

public class TheWorld : MonoBehaviour
{
	public static TheWorld instance;

	public static WorldTimer timer;

	public static WorldNoteRef noteRef;

	public static Memory memory;

	public static CardPool cardPool;
    
    public static BoxUI boxUI;

    public static GameFlow gameFlow;

    public static CardCanvas cardCanvas;

    public static Canvas eventCanvas;

    public static TriggerMgr triggerMgr;

    public static LogCenter logCenter;

    public CardCanvas _cardCanvas;

    public Canvas _eventCanvas;

    void Awake()
	{
		instance = this;
		timer = GetComponent<WorldTimer>();
		noteRef = GetComponent<WorldNoteRef>();
		memory = GetComponent<Memory>();
		cardPool = GetComponent<CardPool>();
        triggerMgr = GetComponent<TriggerMgr>();
        cardCanvas = _cardCanvas;
        eventCanvas = _eventCanvas;
	}
}
