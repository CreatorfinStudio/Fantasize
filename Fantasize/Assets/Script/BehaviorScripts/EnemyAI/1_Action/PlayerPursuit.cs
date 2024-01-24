using BehaviorDesigner.Runtime.Tasks;
using Definition;
using UnityEngine;

namespace AI
{
    public class PlayerPursuit : Action
    {
        [Header("정찰 종료 판정 거리")]
        public float distance = 1.2f;
        public override TaskStatus OnUpdate()
        {
            Transform transform = this.transform;
            transform.position = Vector3.MoveTowards(transform.position,
                DefinitionManager.Instance.player.transform.position, DefinitionManager.Instance.imonsterInfo.GetMoveSpeed() * Time.deltaTime);

            var tmp = transform.position;
            tmp.y = 0;
            transform.position = tmp;

            Debug.Log(Mathf.Abs(transform.position.x - DefinitionManager.Instance.player.transform.position.x));
            if (Mathf.Abs(transform.position.x - DefinitionManager.Instance.player.transform.position.x) <= distance)
                return TaskStatus.Success; // 목표에 도착했으면 Success 반환

            return TaskStatus.Running;
        }
    }
}