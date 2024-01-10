using BehaviorDesigner.Runtime.Tasks;
using Definition;
using UnityEngine;

namespace PlayerBehavior
{
    public class Attack : Action
    {
        public override void OnStart()
        {           
        }
        public override TaskStatus OnUpdate()
        {
            var attackType = DefinitionManager.Instance.iplayerInfo.GetAttackType();

            Debug.LogError(attackType);

            //attackType�� ���� �ִϸ��̼� ����ϵ��� ��ƼŬ�ֵ��� ���
            return TaskStatus.Success;
        }

    }
}