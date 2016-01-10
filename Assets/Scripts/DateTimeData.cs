using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class DateTimeData
{
    public int year;
    public int month;
    public int day;

    public DateTimeData(int y, int m, int d)
    {
        year = y;
        month = m;
        day = d;
    }

    public bool IsSameTime(DateTime rhs)
    {
        return year == rhs.Year && month == rhs.Month && day == rhs.Day;
    }

    public DateTime dateTime
    {
        get {
            return new DateTime(year, month, day);
        }
    }
}