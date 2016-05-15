using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class LogPage : Page
{
    public List<Text> txtList = new List<Text>();

    public override void SetPage(int idx)
    {
        var logs = TheWorld.logCenter.logs;

        for (int i = 0; i < txtList.Count; i++)
        {
            var logidx = txtList.Count * idx + i;
            var txt = txtList[i];

            if (logs.logs.Count <= logidx)
            {
                txt.gameObject.SetActive(false);
            }
            else
            {
                txt.text = logs.logs[logidx].txt;
                txt.gameObject.SetActive(true);
            }
        }
    }
}
