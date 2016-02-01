using UnityEngine;
using System.Collections;

public class AnimatorPlay : Trigger.Event
{
    public string stateName;

    public override void Fire(ActionBlock block, Trigger trigger)
    {
        var anim = block.context.actor.GetComponent<Animator>();

        anim.Play(stateName);
    }
}
