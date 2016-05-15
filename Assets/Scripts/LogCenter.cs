using UnityEngine;
using System.Collections.Generic;

public class LogCenter : MonoBehaviour
{
    [System.Serializable]
    public class Log
    {
        public string txt = "";

        public Log(string txt)
        {
            this.txt = txt;
        }
    };

    [System.Serializable]
    public class Logs
    {
        public List<Log> logs = new List<Log>();
    }

    [System.NonSerialized]
    public Logs logs = new Logs();

    public void LeaveLog(Log log)
    {
        logs.logs.Add(log);
    }

    void Awake ()
    {
        TheWorld.logCenter = this;
	}
}
