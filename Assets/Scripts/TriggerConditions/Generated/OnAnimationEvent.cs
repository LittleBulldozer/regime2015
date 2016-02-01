using UnityEngine;
using System.Collections;

public class OnAnimationEvent : Trigger.Condition, IAnimationEventReceiver
{
    public AnimationEventTypeData eventTypeData;

    public override void Init(ActionBlock block, Trigger trigger)
    {
        this.block = block;
        this.trigger = trigger;

        var target = block.context.actor;
        target.AddComponent<AnimationEventReflector>();
        var reflector = target.GetComponent<AnimationEventReflector>();
        reflector.target = this;
    }

    public override void Release()
    {
        var target = block.context.actor;
        Destroy(target.GetComponent<AnimationEventReflector>());
    }

    ActionBlock block;
    Trigger trigger;

    void HandleEvent(string eventName)
    {
		if (eventName == eventTypeData.name)
		{
			satisfy.Value = true;

            block.context.player.StartCoroutine(DisalbeOnNextTick());
		}
    }

    IEnumerator DisalbeOnNextTick()
    {
        yield return null;

        satisfy.Value = false;
    }

    public void Footstep()
	{
		HandleEvent("Footstep");
	}
	
	public void SkillOver()
	{
		HandleEvent("SkillOver");
	}
	
	public void HideWeapon()
	{
		HandleEvent("HideWeapon");
	}
	
	public void Test()
	{
		HandleEvent("Test");
	}
}
