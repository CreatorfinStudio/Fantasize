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

            //attackType에 따라서 애니메이션 재생하든지 파티클넣든지 등등
            return TaskStatus.Success;
        }

    }
}