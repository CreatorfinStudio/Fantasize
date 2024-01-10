using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using Definition;

namespace AI
{
    public class M_Attack : Action
    {
        [Header("���� �ݶ��̴� ON �����ð�")]
        [SerializeField]
        private float attackDuration = 0.5f; // ���� ���� �ð�
        private float startTime;

        private SpriteRenderer spriteRenderer;
        public override void OnStart()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            startTime = Time.time;
            //�ٶ󺸰� �ִ� ���⿡ �°� �ݶ��̴� ON
            DefinitionManager.Instance.imonsterInfo.GetAttackCollider()[spriteRenderer.flipX ? 0 : 1].enabled = true;
        }

        public override TaskStatus OnUpdate()
        {
            if (Time.time - startTime >= attackDuration)
            {
                //�� ���� �ݶ��̴��� �ٲ�������
                for (int i = 0; i < DefinitionManager.Instance.imonsterInfo.GetAttackCollider().Length; i++)
                    DefinitionManager.Instance.imonsterInfo.GetAttackCollider()[i].enabled = false;

                return TaskStatus.Success;
            }

            return TaskStatus.Running;
        }

        public override void OnEnd()
        {          
            //�� ���� �ݶ��̴��� �ٲ�������
            for (int i = 0; i < DefinitionManager.Instance.imonsterInfo.GetAttackCollider().Length; i++)
                DefinitionManager.Instance.imonsterInfo.GetAttackCollider()[i].enabled = false;
        }
    }
}
