using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

namespace PlayerBehavior
{
    public class IsMove : Conditional
    {
        public override TaskStatus OnUpdate()
        {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                return TaskStatus.Success;
            }
            return TaskStatus.Failure;
        }
    }
}