using UnityEngine;

[System.Serializable]
public class RotateEvent : Trigger.Event
{
    public Vector3 angle;

    public override void Fire(ActionBlock block, Trigger trigger)
    {
        var dt = Time.deltaTime;
        var target = block.context.actor;
        target.transform.Rotate(angle.x * dt, angle.y * dt, angle.z * dt);
    }
}
