public class PreserveTrigger : Trigger.Event
{
    public override void Fire(ActionBlock block, Trigger trigger)
    {
        trigger.preserve = true;
    }
}
