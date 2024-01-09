using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using Definition;

namespace PlayerBehavior
{
    public class IsJump : Conditional
    {
        public override TaskStatus OnUpdate()
        {                
            if (Input.GetKeyDown(KeyCode.Space))
            {
                return TaskStatus.Success;
            }
            return TaskStatus.Failure;
        }
    }
}