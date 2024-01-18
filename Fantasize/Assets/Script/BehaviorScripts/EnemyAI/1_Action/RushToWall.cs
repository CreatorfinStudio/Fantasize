using BehaviorDesigner.Runtime.Tasks;
using Definition;
using UnityEngine;

namespace AI
{
    public class RushToWall : Action
    {
        private float currentSpeed; // 현재 속도

        bool isDirectionChecked = false;
        Vector3 moveDirection;

        public override void OnStart()
        {
            currentSpeed = 0;
            isDirectionChecked = false;
        }
        public override TaskStatus OnUpdate()
        {
            if (!DefinitionManager.Instance.imonsterInfo.GetIsCanRush())
            {
                return TaskStatus.Success;
            }

            if (!isDirectionChecked)
            {
                DefinitionManager.Instance.imonsterInfo.SetIsDirectionCheck(false);

                if (DefinitionManager.Instance.player.transform.position.x <
                    this.transform.position.x)
                {
                    moveDirection = Vector3.left;
                }
                else
                {
                    moveDirection = Vector3.right;
                }

                isDirectionChecked = true;
            }
            // 지정된 방향으로 점차 가속
            currentSpeed += DefinitionManager.Instance.imonsterInfo.GetRushSpeed() * Time.deltaTime;
            transform.position += moveDirection * currentSpeed * Time.deltaTime;


            return TaskStatus.Running;
        }

        public override void OnEnd()
        {
            DefinitionManager.Instance.imonsterInfo.SetIsDirectionCheck(true);
        }
    }
}