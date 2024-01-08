using BehaviorDesigner.Runtime.Tasks;
using Definition;
using UnityEngine;

namespace PlayerBehavior
{
    public class Block : Action
    {
        public override TaskStatus OnUpdate()
        {
            //방어 성공,실패 여부에 따라 이후 효과
            return TaskStatus.Success;
        }
    }
}