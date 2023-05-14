using _SYSTEMS_.Extension;
using UnityEngine;

namespace _SYSTEMS_._Interaction_System_.Abstract
{
    [CreateAssetMenu(menuName = "Interaction Base/Create GameObject Collectable", fileName = "GameObject Collectable", order = 0)]
    public class UsableInteraction : InteractionBase<IUsable>, ICollectableBase
    {
        public override void TouchInteract(IUsable touchObject)
        {
            touchObject.Use();
        }

        public override void ExitInteract(IUsable touchObject)
        {
            touchObject.StopUse();
        }

        public void Effect()
        {
            "Effect".Log(SystemsEnum.GameSystem);
        }

        public void Collect()
        {
            "Collect".Log(SystemsEnum.GameSystem);
        }
    }
}