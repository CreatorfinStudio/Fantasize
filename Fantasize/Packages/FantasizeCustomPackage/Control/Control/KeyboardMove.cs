using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Definition;

namespace Control
{
    public class KeyboardMove : Controller
    {        
        private float h;
        private float v;

        private Animator animator;   


        //모듈 목적에 따라 Start , Anim 구조는 수정해야 할 필요있음
        protected override void Start()
        {
            base.Start();
            animator = AnimationManager.GetAnimator(this.gameObject);
        }
        private void Update()
        {
            Move(AnimationManager.BoolAnim);            
        }

        private void Move(Action<Animator, string, bool> action)
        {
            h = Input.GetAxis("Horizontal");
            v = Input.GetAxis("Vertical");

            Vector3 moveDirection = new Vector3(h, 0f, v).normalized;
            if (moveDirection.magnitude > 0.1f)
            {
                transform.Translate(moveDirection * iplayerInfo.GetMoveSpeed() * Time.deltaTime, Space.World);
                action?.Invoke(animator, "Walk",true);
            }
            else
                action?.Invoke(animator, "Walk",false);
        }

    }
}