using BehaviorDesigner.Runtime.Tasks;
using Definition;
using UnityEngine;

namespace PlayerBehavior
{
    public class Block : Action
    {
        public override TaskStatus OnUpdate()
        {
            //��� ����,���� ���ο� ���� ���� ȿ��
            return TaskStatus.Success;
        }
    }
}