using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Definition;
using static UnityEngine.GraphicsBuffer;

namespace AI
{
    public class RushToWall : Action
    {
        private Rigidbody2D rb; 
        private Vector3 rushDirection;
        Vector2 playerPosition;
        private int direction = 1;
        private SpriteRenderer spriteRenderer;

        public override void OnStart()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            rb = GetComponent<Rigidbody2D>();

            playerPosition = DefinitionManager.Instance.player.transform.position;

            if (transform.position.x <= playerPosition.x)
            {
                direction = 1;
                spriteRenderer.flipX = false;
            }
            else// if (transform.position.x > playerPosition.x)
            { 
                direction = -1;
                spriteRenderer.flipX = true;
            }

            ///Fly Monster
            //rushDirection = (playerPosition - (Vector2)transform.position).normalized;
        }

        public override TaskStatus OnUpdate()
        {
            if (rb == null)
            {
                return TaskStatus.Failure;
            }

            if (DefinitionManager.Instance.imonsterInfo.GetIsCanRush())
            {
                //rb.MovePosition(transform.position + rushDirection *
                //    DefinitionManager.Instance.imonsterInfo.GetRushSpeed() * Time.deltaTime);

                rb.AddForce(new Vector2(DefinitionManager.Instance.imonsterInfo.GetRushSpeed() * direction,
                    rb.velocity.y));

                //rb.velocity = new Vector2(DefinitionManager.Instance.imonsterInfo.GetRushSpeed() * direction,
                //    rb.velocity.y);
                return TaskStatus.Running;
            }
            else
                return TaskStatus.Success;
        }
    }
}