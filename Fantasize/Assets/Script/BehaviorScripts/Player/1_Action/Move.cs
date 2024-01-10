using BehaviorDesigner.Runtime.Tasks;
using Definition;
using UnityEngine;

namespace PlayerBehavior
{
    public class Move : Action
    {
        private float h;

        public override void OnStart()
        {
            h = Input.GetAxis("Horizontal");
        }
        public override TaskStatus OnUpdate()
        {
            Vector2 moveDirection = new Vector3(h, 0f);
            moveDirection.Normalize();
            transform.Translate(moveDirection * DefinitionManager.Instance.iplayerInfo.GetRunSpeed() * Time.deltaTime);

            return TaskStatus.Success;
        }
    }
}