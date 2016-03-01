using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DayText : MonoBehaviour
{
    void Awake()
    {
        TheWorld.timer.day.Listen(DayChanged);
        text = GetComponent<Text>();
        formatString = text.text;
    }

    public void DayChanged(int day, int prev)
    {
        text.text = string.Format(formatString, day);
    }

    string formatString;
    Text text;
}
