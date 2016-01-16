public class Always : Trigger.Condition
{
    public override void Init(ActionBlock block, Trigger trigger)
    {
        satisfy.Value = true;
    }
}
