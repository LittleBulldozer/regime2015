using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UITurnAlert : MonoBehaviour
{
    public void SetString(int year, int month, int day, int nrTurn)
    {
        dateTxt.text = string.Format(dateStr, year, month, day);
        turnTxt.text = string.Format(turnStr, nrTurn);
    }

    [SerializeField]
    Text dateTxt;

    [SerializeField]
    Text turnTxt;

    string dateStr;
    string turnStr;

    void Awake()
    {
        dateStr = dateTxt.text;
        turnStr = turnTxt.text;
    }
}
