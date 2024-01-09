using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using Definition;

namespace PlayerBehavior
{
    public class IsAttack : Conditional
    {
        public override TaskStatus OnUpdate()
        {
            if (Input.GetKeyUp(KeyCode.D))
            {
                return TaskStatus.Success;
            }
            return TaskStatus.Failure;
        }
    }
}