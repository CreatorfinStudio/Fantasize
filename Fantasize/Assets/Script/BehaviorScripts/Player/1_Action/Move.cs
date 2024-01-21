using BehaviorDesigner.Runtime.Tasks;
using Definition;
using UnityEngine;

namespace PlayerBehavior
{
    public class Move : Action
    {
        private Animator animator;

        public override void OnStart()
        {
            animator = GetComponent<Animator>();
        }

        public override TaskStatus OnUpdate()
        {
            float h = Input.GetAxis("Horizontal");

            if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
                && !DefinitionManager.Instance.iplayerInfo.GetIsDashing())
            {
                animator.SetBool("Running", true); // 달리기 상태 활성화
                Vector2 moveDirection = new Vector2(h, 0f);
                moveDirection.Normalize();
                transform.Translate(moveDirection * DefinitionManager.Instance.iplayerInfo.GetMoveSpeed() * Time.deltaTime);

                return TaskStatus.Success;
            }
            else
            {
                animator.SetBool("Running", false); // 달리기 상태 비활성화
                return TaskStatus.Failure;
            }
        }
    }
}
