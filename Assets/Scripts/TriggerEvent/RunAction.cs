using UnityEngine;
using System.Collections;
using System;

public class RunAction : Trigger.Event
#if UNITY_EDITOR
    , IChildActionNameSuggestable
#endif
{
    public Action action;
    public ActorInfo actorInfo;
    public ChildActionCreationName childActionName;

    public override void Fire(ActionBlock block, Trigger trigger)
    {
        this.block = block;

        var actor = GetActor();
        actor.AddComponent<ActionPlayer>();
        var player = actor.GetComponent<ActionPlayer>();
        player.action = action;
        player.Play();
    }

    ActionBlock block;

#if UNITY_EDITOR
    public string _ChildActionName
    {
        get
        {
            return childActionName.name;
        }
    }
#endif

    GameObject GetActor()
    {
        switch (actorInfo.type)
        {
            case ActorInfo.ActorRefType.SELF:
                return block.context.actor;

            case ActorInfo.ActorRefType.CHILD:
                return block.context.actor.transform.Find(actorInfo.name).gameObject;

            case ActorInfo.ActorRefType.PARENT:
                return block.context.actor.transform.parent.gameObject;

            default:
                throw new System.Exception("Unhandled actor info type : " + actorInfo.type);
        }
    }
}
