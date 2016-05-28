using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class DateText : MonoBehaviour
{
    public enum What
    {
        YEAR
        , MONTH
        , DAY
    }

    public WorldTimer timer;
    public What what;

    Text text;

	void Awake ()
    {
        text = GetComponent<Text>();

        timer.dateTime.Listen(DateTimeChanged);
	}

    void DateTimeChanged(DateTime dateTime, DateTime prevDateTime)
    {
        switch (what)
        {
            case What.YEAR:
                text.text = string.Format("{0}", dateTime.Year);
                break;

            case What.MONTH:
                text.text = string.Format("{00:0}", dateTime.Month);
                break;

            case What.DAY:
                text.text = string.Format("{00:0}", dateTime.Day);
                break;
        }
        
    }
}
