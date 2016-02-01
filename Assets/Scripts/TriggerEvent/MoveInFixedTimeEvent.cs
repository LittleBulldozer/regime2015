using UnityEngine;
using System.Collections;

public class MoveInFixedTimeEvent : Trigger.Event
{
    public AnchorInfo destination;
    public float duration;

    public override void Fire(ActionBlock block, Trigger trigger)
    {
        var target = block.context.actor;
        target.AddComponent<MoveInFixedTime>();
        var moveCompo = target.GetComponent<MoveInFixedTime>();
        moveCompo.destination = destination.Get();
        moveCompo.duration = duration;
    }
}
