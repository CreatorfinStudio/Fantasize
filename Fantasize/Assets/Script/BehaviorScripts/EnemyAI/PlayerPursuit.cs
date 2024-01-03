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

            var tmp = transform.position;
            tmp.y = 0;
            transform.position = tmp;

            // 목표지점에 도착했는지 확인

            Debug.Log(Mathf.Abs(transform.position.x - DefinitionManager.Instance.player.transform.position.x));
            if (Mathf.Abs(transform.position.x - DefinitionManager.Instance.player.transform.position.x) < .5f) // 도착 판정 거리
            {
                return TaskStatus.Success; // 목표에 도착했으면 Success 반환
            }

            return TaskStatus.Running; 
        }
    }
}