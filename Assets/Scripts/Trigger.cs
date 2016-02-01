using UnityEngine;
using System.Collections.Generic;
using System;


public class Trigger
{
#if UNITY_EDITOR
    public interface IEditorReferencable
    {
        Trigger _TriggerRef
        {
            get;
        }

        ActionBlock _BlockRef
        {
            get;
        }
    }
#endif

    [System.Serializable]
    public class Condition : ScriptableObject
#if UNITY_EDITOR
        , IEditorReferencable
#endif
    {
        public Observable<bool> satisfy = new Observable<bool>(false);

        public virtual void Init(ActionBlock block, Trigger trigger)
        { }

        public virtual void Release()
        { }

#if UNITY_EDITOR
        public Trigger _TriggerRef
        {
            get
            {
                return _triggerRef;
            }

            set
            {
                _triggerRef = value;
            }
        }

        public ActionBlock _BlockRef
        {
            get
            {
                return _blockRef;
            }

            set
            {
                _blockRef = value;
            }
        }

        Trigger _triggerRef;
        ActionBlock _blockRef;
#endif
    }

    [System.Serializable]
    public class Event : ScriptableObject
#if UNITY_EDITOR
        , IEditorReferencable
#endif
    {
        public virtual void Fire(ActionBlock block, Trigger trigger)
        { }

#if UNITY_EDITOR
        public Trigger _TriggerRef
        {
            get
            {
                return _triggerRef;
            }

            set
            {
                _triggerRef = value;
            }
        }

        public ActionBlock _BlockRef
        {
            get
            {
                return _blockRef;
            }

            set
            {
                _blockRef = value;
            }
        }

        Trigger _triggerRef;
        ActionBlock _blockRef;
#endif
    }

    public bool preserve = false;
    public bool alive = false;
    List<Condition> conditions = new List<Condition>();
    List<Event> events = new List<Event>();

    public Trigger(ActionBlock block, TriggerDesc desc)
    {
        this.block = block;

        foreach (var cond in desc.conditions)
        {
            var newCond = UnityEngine.Object.Instantiate(cond);
            conditions.Add(newCond);
        }

        foreach (var e in desc.events)
        {
            var newEvent = UnityEngine.Object.Instantiate(e);
            events.Add(newEvent);
        }

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

            condListenId = cond.satisfy.Listen((x, prevX) =>
                {
                    if (x == true && prevX == false)
                    {
                        condSatisfyCount++;
                        CheckSatisfaction();
                    }
                    else if (x == false && prevX == true)
                    {
                        condSatisfyCount--;
                        satisfyAll.Value = false;
                    }
                }
            );
        }

        alive = true;
    }

    public void Release()
    {
        if (alive == false)
        {
            return;
        }

        alive = false;

        satisfyAll.Value = false;

        foreach (var cond in conditions)
        {
            cond.Release();

            cond.satisfy.StopListen(condListenId);
        }
    }
    
    int condSatisfyCount;
    int condListenId;

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

/*    void Update()
    {
        var blk = transform.parent.GetComponent<ActionBlock>();
        foreach (var e in events)
        {
            e._BlockRef = blk;
            e._TriggerRef = this;
        }
    }
    */
#endif
}
