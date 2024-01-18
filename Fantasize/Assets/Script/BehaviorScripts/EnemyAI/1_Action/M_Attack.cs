using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using Definition;

namespace AI
{
    public class M_Attack : Action
    {
        [Header("무기 콜라이더 ON 유지시간")]
        [SerializeField]
        private float attackDuration = 0.5f; // 공격 지속 시간
        private float startTime;

        public override void OnStart()
        {
            startTime = Time.time;
            //바라보고 있는 방향에 맞게 콜라이더 ON
          //  DefinitionManager.Instance.imonsterInfo.GetAttackCollider()[spriteRenderer.flipX ? 0 : 1].enabled = true;
        }

        public override TaskStatus OnUpdate()
        {
            if (Time.time - startTime >= attackDuration)
            {
                //걍 무기 콜라이더는 다꺼버리기
                //for (int i = 0; i < DefinitionManager.Instance.imonsterInfo.GetAttackCollider().Length; i++)
                //    DefinitionManager.Instance.imonsterInfo.GetAttackCollider()[i].enabled = false;

                return TaskStatus.Success;
            }

            return TaskStatus.Running;
        }

        //public override void OnEnd()
        //{          
        //    // 무기 콜라이더는 다꺼버리기
        //    DefinitionManager.Instance.imonsterInfo.GetAttackCollider().enabled = false;
        //}
    }
}
