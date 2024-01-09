using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Definition;
using static UnityEngine.GraphicsBuffer;
using Unity.VisualScripting;

namespace AI
{
    public class RushToWall : Action
    {
        private float currentSpeed; // 현재 속도
        private SpriteRenderer spriteRenderer;

        bool isDirectionChecked = false;
        Vector3 moveDirection;

        public override void OnStart()
        {           
            currentSpeed = 0;
            isDirectionChecked = false;
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        public override TaskStatus OnUpdate()
        {
            if (!DefinitionManager.Instance.imonsterInfo.GetIsCanRush())
            {
                return TaskStatus.Success;
            }

            if (!isDirectionChecked)
            {
                DefinitionManager.Instance.imonsterInfo.SetIsSpriteCheck(false);

                if (DefinitionManager.Instance.player.transform.position.x <
                    this.transform.position.x)
                {
                    moveDirection = Vector3.left;
                    spriteRenderer.flipX = true;
                }
                else
                {
                    moveDirection = Vector3.right;
                    spriteRenderer.flipX = false;
                }
                isDirectionChecked = true;
            }
            // 지정된 방향으로 점차 가속
            currentSpeed += DefinitionManager.Instance.imonsterInfo.GetRushSpeed() * Time.deltaTime;
            transform.position += moveDirection * currentSpeed * Time.deltaTime;          


            return TaskStatus.Running;       

        }
    }
}