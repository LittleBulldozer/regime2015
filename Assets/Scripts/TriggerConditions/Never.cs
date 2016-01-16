public class Never : Trigger.Condition
{
    public override void Init(ActionBlock block, Trigger trigger)
    {
        satisfy.Value = false;
    }
}
