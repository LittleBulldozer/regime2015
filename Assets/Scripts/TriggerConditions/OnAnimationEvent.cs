using UnityEngine;

public enum AnimatedObjectType
{
    ACTOR,
    ACTOR_PARENT,
    CASTER
}

public enum AnimationEventType
{
    VFX001 = 100,
    VFX002,
    VFX003,
    VFX004,
    VFX005,
    VFX006,
    VFX007,
    VFX008,
    VFX009
}

public class OnAnimationEvent : Trigger.Condition
{
    public AnimatedObjectType target;
    public AnimationEventType eventName;

    public override void Init(ActionBlock block, Trigger trigger)
    {
        

    }

    //    public override void Release()
    //    { }

}
