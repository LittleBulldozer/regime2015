using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class DateText : MonoBehaviour
{
    public WorldTimer timer;

    Text text;

	void Awake ()
    {
        text = GetComponent<Text>();

        timer.dateTime.Listen(DateTimeChanged);
	}

    void DateTimeChanged(DateTime newDateTime)
    {
        text.text = string.Format("{0}-{1}-{2}",
            newDateTime.Year, newDateTime.Month, newDateTime.Day);
    }
}
