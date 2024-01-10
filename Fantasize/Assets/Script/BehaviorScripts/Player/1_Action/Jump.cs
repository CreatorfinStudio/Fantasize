using BehaviorDesigner.Runtime.Tasks;
using Definition;
using UnityEngine;

namespace PlayerBehavior
{
    public class Jump : Action
    {
        private Rigidbody2D rb;
        private float h;
        public override void OnStart()
        {
            rb = GetComponent<Rigidbody2D>();
            h = Input.GetAxis("Horizontal");
        }
        public override TaskStatus OnUpdate()
        {
            if (DefinitionManager.Instance.iplayerInfo.GetIsCanJump())
            {
                //이미 점프중이면 방향만 조정
                if (DefinitionManager.Instance.iplayerInfo.GetIsJumping())
                {
                    rb.velocity =
                   new Vector2(h * DefinitionManager.Instance.iplayerInfo.GetRunSpeed(), rb.velocity.y);
                }         
                else
                {
                    DefinitionManager.Instance.iplayerInfo.SetIsCanJump(false);
                    DefinitionManager.Instance.iplayerInfo.SetIsJumping(true);

                    rb.velocity =
                        new Vector3(rb.velocity.x, DefinitionManager.Instance.iplayerInfo.GetJumpForce());
                }
                return TaskStatus.Success;
            }
            return TaskStatus.Failure;
        }
    }
}