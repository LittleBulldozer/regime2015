using UnityEngine;
using System.Collections.Generic;

public abstract class IActionBlock
{
    public float duration = 1.0f;

    public abstract bool Begin(Action action);
    public abstract bool UpdateTime(float time, float deltaTime);
    public abstract bool End();
}

public class ActionBlock : MonoBehaviour//, IActionBlock
{
    [SerializeField]
    public float duration = 1.0f;

    [SerializeField]
    public bool isLoop = false;

    public GameObject Actor
        {
        get {
            return actor;
        }
    }

    [System.NonSerialized]
    public List<Trigger> triggers;

    public System.Action onBegan;
    public System.Action onEnd;
    public System.Action<float, float> onUpdate;

    public void SetForceOver()
    {
        forceOver = true;
    }

    public bool Begin(Action action)
    {
        actor = action.actor;

        InitTriggers();

        if (onBegan != null)
        {
            onBegan();
        }

        return !forceOver; 
    }

    public bool End()
    {
        if (forceOver == false && isLoop == true)
        {
            return false;
        }
        
        if (onEnd != null)
        {
            onEnd();
        }

        return true;
    }

    public bool UpdateTime(float time, float deltaTime)
    {
        if (onUpdate != null)
        {
            onUpdate(time, deltaTime);
        }

        return !forceOver;
    }

    GameObject actor;
    bool forceOver = false;

    void Awake()
    {
        triggers = new List<Trigger>();

        foreach (var trigger in GetComponentsInChildren<Trigger>())
        {
            triggers.Add(trigger);
        }
    }

    void InitTriggers()
    {
        foreach (var trigger in triggers)
        {
            trigger.Init(this);
        }
    }
}