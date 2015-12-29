using UnityEngine;
using System.Collections;
using System;

public class WorldTimer : MonoBehaviour
{
    public enum TimeScaleMode
    {
        PAUSE,
        NORMAL,
        FAST,
        FASTEST
    }

    public float daysPerSecs = 2.0f;
    public TimeScaleMode timeScaleMode = TimeScaleMode.NORMAL;
    public Observable<int> day = new Observable<int>(0);
    public Observable<DateTime> dateTime = new Observable<DateTime>();

    public void SetTimeScaleModeFastest()
    {
        timeScaleMode = TimeScaleMode.FASTEST;
    }

    public void SetTimeScaleModeFast()
    {
        timeScaleMode = TimeScaleMode.FAST;
    }

    public void SetTimeScaleModeNormal()
    {
        timeScaleMode = TimeScaleMode.NORMAL;
    }

    public void SetTimeScaleModePause()
    {
        timeScaleMode = TimeScaleMode.PAUSE;
    }

    float dayF = 0.0f;
    int cachedDays = 0;

    void Awake()
    {
        dateTime.Value = DateTime.Today;
    }

	// Update is called once per frame
	void Update ()
    {
        float dt = daysPerSecs * Time.deltaTime;

        switch (timeScaleMode)
        {
            case TimeScaleMode.PAUSE:
                break;

            case TimeScaleMode.NORMAL:
                dayF += dt;
                break;

            case TimeScaleMode.FAST:
                dayF += 2 * dt;
                break;

            case TimeScaleMode.FASTEST:
                dayF += 3 * dt;
                break;
        }

        int nowDay = Mathf.CeilToInt(dayF);
        if (nowDay > day.Value)
        {
            if (nowDay - day.Value >= 2)
            {   
                Debug.LogError("Invalid delta day : " + (nowDay - day.Value));
            }

            day.Value = nowDay;

            var deltaDay = day.Value - cachedDays;
            if (deltaDay >= 1)
            {
                dateTime.Value = dateTime.Value.AddDays(deltaDay);

                cachedDays = day.Value;
            }
        }
    }
}
