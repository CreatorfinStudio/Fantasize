using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using Definition;

namespace PlayerBehavior
{
    public class IsMove : Conditional
    {
        public override TaskStatus OnUpdate()
        {
            if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
                && !DefinitionManager.Instance.iplayerInfo.GetIsDashing())
            {
                return TaskStatus.Success;
            }
            return TaskStatus.Failure;
        }
    }
}