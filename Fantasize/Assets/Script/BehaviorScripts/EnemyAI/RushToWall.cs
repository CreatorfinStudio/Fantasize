using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Definition;

namespace AI
{
    public class RushToWall : Action
    {
        private Rigidbody2D rb; 
        private Vector3 rushDirection; 

        public override void OnStart()
        {
            DefinitionManager.Instance.imonsterInfo.SetIsCanRush(true);
            rb = GetComponent<Rigidbody2D>();
  
            Vector2 playerPosition = DefinitionManager.Instance.player.transform.position;
            rushDirection = (playerPosition - (Vector2)transform.position).normalized;
        }

        public override TaskStatus OnUpdate()
        {
            if (rb == null)
            {
                return TaskStatus.Failure;
            }

            if (DefinitionManager.Instance.imonsterInfo.GetIsCanRush())
            {
                rb.MovePosition(transform.position + rushDirection * 
                    DefinitionManager.Instance.imonsterInfo.GetRushSpeed() * Time.deltaTime);
                return TaskStatus.Running;
            }
            else
                return TaskStatus.Success;
        }
    }
}