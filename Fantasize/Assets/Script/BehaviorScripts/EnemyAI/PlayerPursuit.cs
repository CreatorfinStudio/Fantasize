using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using Definition;

namespace AI
{
    public class PlayerPursuit : Action
    {
        public override TaskStatus OnUpdate()
        {
            Transform transform = this.transform; 
            transform.position = Vector3.MoveTowards(transform.position,
                DefinitionManager.Instance.player.transform.position, DefinitionManager.Instance.imonsterInfo.GetMoveSpeed() * Time.deltaTime);

            // ��ǥ������ �����ߴ��� Ȯ��
            if (Vector3.Distance(transform.position, DefinitionManager.Instance.player.transform.position) < 0.1f) // ���� ���� �Ÿ�
            {
                return TaskStatus.Success; // ��ǥ�� ���������� Success ��ȯ
            }

            return TaskStatus.Running; 
        }
    }
}