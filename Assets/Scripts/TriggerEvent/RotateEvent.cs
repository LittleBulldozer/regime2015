using UnityEngine;

[System.Serializable]
public class RotateEvent : Trigger.Event
{
    public Vector3 angle;

    public override void Fire(ActionBlock block, Trigger trigger)
    {
        var dt = Time.deltaTime;
        block.Actor.transform.Rotate(angle.x * dt, angle.y * dt, angle.z * dt);
    }
}
