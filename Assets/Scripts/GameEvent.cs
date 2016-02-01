using UnityEngine;
using System;
using UnityEngine.Events;
using System.Collections;

public class GameEvent : MonoBehaviour
{
    public GameObject world;
    public DateTimeData dateData;
    public UnityEvent unityEvent;
    

    WorldTimer timer;
    int listenerId;

    void Awake()
    {
        timer = world.GetComponent<WorldTimer>();
        listenerId = timer.dateTime.Listen(DateTimeListener);
    }

    void OnDestroy()
    {
        timer.dateTime.StopListen(listenerId);
    }

    void DateTimeListener(DateTime nowTime, DateTime prevTime)
    {
        if (dateData.IsSameTime(nowTime))
        {
            unityEvent.Invoke();

            Destroy(this);
        }
    }
}
