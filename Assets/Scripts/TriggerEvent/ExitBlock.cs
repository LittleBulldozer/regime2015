using UnityEngine;
using System.Collections;

public class ExitBlock : Trigger.Event
{
    public override void Fire(ActionBlock block, Trigger trigger)
    {
        block.SetForceOver();
    }
}
