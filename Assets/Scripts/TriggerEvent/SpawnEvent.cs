using UnityEngine;

public class SpawnEvent : Trigger.Event
{
    public GameObject something;

    public override void Fire(ActionBlock block, Trigger trigger)
    {
        var newObj = Instantiate(something);
    }
}
