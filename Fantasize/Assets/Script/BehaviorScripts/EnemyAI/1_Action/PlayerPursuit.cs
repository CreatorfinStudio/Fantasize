using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using Definition;

namespace AI
{
    public class PlayerPursuit : Action
    {
        [Header("���� ���� ���� �Ÿ�")]
        public float distance = 1.2f;
        public override TaskStatus OnUpdate()
        {
            if (DefinitionManager.Instance.player != null)
            {
                Transform transform = this.transform;
                transform.position = Vector3.MoveTowards(transform.position,
                    DefinitionManager.Instance.player.transform.position, DefinitionManager.Instance.imonsterInfo.GetMoveSpeed() * Time.deltaTime);

                var tmp = transform.position;
                tmp.y = 0;
                transform.position = tmp;

                Debug.Log(Mathf.Abs(transform.position.x - DefinitionManager.Instance.player.transform.position.x));
                if (Mathf.Abs(transform.position.x - DefinitionManager.Instance.player.transform.position.x) <= distance)
                    return TaskStatus.Success; // ��ǥ�� ���������� Success ��ȯ
            }
            return TaskStatus.Running; 
        }
    }
}