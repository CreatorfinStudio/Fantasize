using BehaviorDesigner.Runtime.Tasks;
using Definition;
using Unity.VisualScripting;
using UnityEngine;

namespace PlayerBehavior
{
    public class Dash : Action
    {
        public override void OnStart()
        {
            if (!DefinitionManager.Instance.iplayerInfo.GetIsDashing())
            {
                DefinitionManager.Instance.iplayerInfo.SetIsDashing(true);               
            }
        }

        public override TaskStatus OnUpdate()
        {
            if (!DefinitionManager.Instance.iplayerInfo.GetIsDashing())
            {
                return TaskStatus.Success;
            }          

            return TaskStatus.Running;
        }
    }
}
