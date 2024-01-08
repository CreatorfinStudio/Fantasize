using BehaviorDesigner.Runtime.Tasks;
using Definition;
using UnityEngine;

namespace PlayerBehavior
{
    public class Dash : Action
    {
        private float startTime;
        private Rigidbody2D rb;

        public override void OnStart()
        {
            DefinitionManager.Instance.iplayerInfo.SetIsDashing(true);
            startTime = Time.time;
            rb = GetComponent<Rigidbody2D>();
        }

        public override TaskStatus OnUpdate()
        {
            if (Time.time - startTime >= 0.6f)
            {
                rb.velocity = Vector2.zero;
                DefinitionManager.Instance.iplayerInfo.SetIsDashing(false);
                DefinitionManager.Instance.iplayerInfo.SetDashDirection(0);
                return TaskStatus.Success;
            }

            rb.velocity = new Vector2(10f * DefinitionManager.Instance.iplayerInfo.GetDashDirection(), rb.velocity.y);

            return TaskStatus.Running;
        }
    }
}
