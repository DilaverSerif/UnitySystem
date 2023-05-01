namespace _SYSTEMS_._Interaction_System_.Abstract
{
    public class UsableInteraction : InteractionBase<IUsable>
    {
        public override void TouchInteract(IUsable touchObject)
        {
            touchObject.Use();
        }

        public override void ExitInteract(IUsable touchObject)
        {
            touchObject.StopUse();
        }
    }
}