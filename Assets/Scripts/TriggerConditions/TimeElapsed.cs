using UnityEngine;
using System.Collections;

public class TimeElapsed : Trigger.Condition
{
    /*    public enum TimeAnchor
        {
            ACTION_BLOCK_START,
            ACTION_START
        }
        public TimeAnchor timeAnchor;
        */
    public float time;

    public override void Init(ActionBlock block, Trigger trigger)
    {
        block.context.player.StartCoroutine(SwitchOn(time));
/*        switch (timeAnchor)
        {
            case TimeAnchor.ACTION_BLOCK_START:

                break;

            case TimeAnchor.ACTION_START:
                break;
        }*/
    }

    IEnumerator SwitchOn(float time)
    {
        yield return new WaitForSeconds(time);

        satisfy.Value = true;
    }
}
