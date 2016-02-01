using UnityEngine;
using System.Collections.Generic;

public class TriggerDesc : ScriptableObject
{
    [SerializeField]
    public List<Trigger.Condition> conditions = new List<Trigger.Condition>();

    [SerializeField]
    public List<Trigger.Event> events = new List<Trigger.Event>();
}
