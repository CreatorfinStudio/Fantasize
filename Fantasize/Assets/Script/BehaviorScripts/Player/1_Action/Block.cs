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
            Debug.LogError("���");
            return TaskStatus.Success;
        }
    }
}