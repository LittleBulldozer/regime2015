using UnityEngine;
using System.Collections.Generic;
using System;

public class Trigger : MonoBehaviour
{
    [System.Serializable]
    public class Condition : ScriptableObject
    {
        public Observable<bool> satisfy = new Observable<bool>(false);

        public virtual void Init(ActionBlock block, Trigger trigger)
        { }

        public virtual void Release()
        { }
    }

    [System.Serializable]
    public class Event : ScriptableObject
    {
        public virtual void Fire(ActionBlock block, Trigger trigger)
        { }
    }

    public bool preserve = false;

    public void Init(ActionBlock block_)
    {
        block = block_;

        satisfyAll.Listen(HandleSatisfaction);

        condSatisfyCount = 0;

        foreach (var cond in conditions)
        {
            cond.Init(block, this);

            if (cond.satisfy.Value == true)
            {
                condSatisfyCount++;
                CheckSatisfaction();
            }

            cond.satisfy.Listen((x, prevX) =>
                {
                    if (x == true && prevX == false)
                    {
                        condSatisfyCount++;
                        CheckSatisfaction();
                    }
                    else if (x == false && prevX == true)
                    {
                        condSatisfyCount--;
                    }
                }
            );
        }
    }

    public void Release()
    {
        satisfyAll.Value = false;

        foreach (var cond in conditions)
        {
            cond.Release();
        }
    }
    
    [SerializeField]
    List<Condition> conditions = new List<Condition>();

    [SerializeField]
    List<Event> events = new List<Event>();

    int condSatisfyCount;

    ActionBlock block;
    Observable<bool> satisfyAll = new Observable<bool>(false);

    void CheckSatisfaction()
    {
        if (condSatisfyCount == conditions.Count)
        {
            satisfyAll.Value = true;
        }
    }

    void TriggerEvents()
    {
        foreach (var E in events)
        {
            E.Fire(block, this);
        }

        if (preserve == false)
        {
            Release();
        }
    }

    void TriggerEventsOnUpdate(float time, float deltaTime)
    {
        TriggerEvents();
    }

    void HandleSatisfaction(bool satisfy, bool _prev)
    {
        if (satisfy == true && _prev == false)
        {
            block.onBegan += TriggerEvents;
            block.onUpdate += TriggerEventsOnUpdate;
            block.onEnd += TriggerEvents;
        }
        else if (satisfy == false && _prev == true)
        {
            block.onBegan -= TriggerEvents;
            block.onUpdate -= TriggerEventsOnUpdate;
            block.onEnd -= TriggerEvents;
        }
    }

#if UNITY_EDITOR
    public List<Condition> _Conditions
    {
        get
        {
            return conditions;
        }
    }

    public List<Event> _Events
    {
        get
        {
            return events;
        }
    }

#endif
}
