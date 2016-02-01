using UnityEngine;
using System.Collections;

public class MoveBySpeedEvent : Trigger.Event
{
    public AnchorInfo destination;
    public float speed;

    public override void Fire(ActionBlock block, Trigger trigger)
    {
        var target = block.context.actor;
        target.AddComponent<MoveBySpeed>();
        var moveCompo = target.GetComponent<MoveBySpeed>();
        moveCompo.destination = destination.Get();
        moveCompo.speed = speed;
    }
}
