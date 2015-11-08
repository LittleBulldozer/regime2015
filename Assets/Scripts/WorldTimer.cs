using UnityEngine;
using System.Collections;
using System;

public class WorldTimer : MonoBehaviour
{
    public enum TimeScaleMode
    {
        PAUSE,
        NORMAL,
        FASTEST
    }

    public float daysPerSecs = 0.5f;
    public TimeScaleMode timeScaleMode = TimeScaleMode.NORMAL;
//    public Observable<int> day = new Observable<int>(0);


    float dayF = 0.0f;

    void Start ()
    {
        DateTime t;
        t = new DateTime(2015, 11, 8);
	}
	
	// Update is called once per frame
	void Update ()
    {
        float dt = Mathf.Min(1.0f, daysPerSecs * Time.deltaTime);
        
        dayF += dt;

/*        int nowDay = Mathf.CeilToInt(dayF);
        if (nowDay > day.Value)
        {
            if (nowDay - day.Value >= 2)
            {
                Debug.LogError("Invalid delta day : " + nowDay - day.Value);
            }
            day.Value = nowDay;
        }*/
    }
}
