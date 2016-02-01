using UnityEngine;
using System.Collections;

public class Arrive : Trigger.Condition
{
    public AnchorInfo destination;
    public float epsilon = 0.1f;

    public override void Init(ActionBlock block, Trigger trigger)
    {
        end = false;
        block.context.player.StartCoroutine(Watch(block.context.actor));
    }

    public override void Release()
    {
        end = true;
    }

    bool end;

    IEnumerator Watch(GameObject actor)
    {
        Transform destT = destination.Get();

        while (end == false)
        {
            if ((actor.transform.position - destT.position).sqrMagnitude <= epsilon)
            {
                satisfy.Value = true;
                break;
            }

            yield return null;
        }
    }
}
