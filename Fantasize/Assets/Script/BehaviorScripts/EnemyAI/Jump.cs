using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using Definition;

namespace AI
{
    public class Jump : Action
    {
        private Rigidbody2D rb;

        public override void OnStart()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public override TaskStatus OnUpdate()
        {
            if (rb == null)
            {
                return TaskStatus.Failure;
            }

            Vector3 moveDirection = rb.velocity.normalized;
            rb.AddForce((Vector3.up + moveDirection) * DefinitionManager.Instance.imonsterInfo.GetJumpForce(), ForceMode2D.Impulse);

            return TaskStatus.Success;
        }

    }
}