using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using Definition;

namespace PlayerBehavior
{
    public class IsBlock : Conditional
    {
        public override TaskStatus OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                return TaskStatus.Success;
            }
            return TaskStatus.Failure;
        }
    }
}