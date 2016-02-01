using UnityEngine;
using System.Collections;

public class Hide : Trigger.Event
{
    public override void Fire(ActionBlock block, Trigger trigger)
    {
        this.block = block;

        var R = block.context.actor.GetComponent<Renderer>();
        if (R == null)
        {
            throw new System.Exception("No Renderer");
        }

        cachedEnableState = R.enabled;
        R.enabled = false;

        block.onEnd += OnBlockEnd;
    }

    ActionBlock block;
    bool cachedEnableState;

    void OnBlockEnd()
    {
        block.onEnd -= OnBlockEnd;

        var R = block.context.actor.GetComponent<Renderer>();
        R.enabled = cachedEnableState;
    }
}
