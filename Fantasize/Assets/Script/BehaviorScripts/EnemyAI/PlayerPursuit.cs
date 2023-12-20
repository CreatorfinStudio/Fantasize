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

            // 목표지점에 도착했는지 확인
            if (Vector3.Distance(transform.position, DefinitionManager.Instance.player.transform.position) < 0.1f) // 도착 판정 거리
            {
                return TaskStatus.Success; // 목표에 도착했으면 Success 반환
            }

            return TaskStatus.Running; 
        }
    }
}