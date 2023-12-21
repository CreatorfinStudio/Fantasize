using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

namespace AI
{
    public class EnemyAction : Action
    {
        protected Rigidbody2D rig;
        protected Animator animator;



        public override void OnAwake()
        {
            rig = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }
    }
}