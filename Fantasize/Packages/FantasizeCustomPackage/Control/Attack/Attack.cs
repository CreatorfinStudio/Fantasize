using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Control
{
    public class Attack : Controller
    {
        protected Animator animator;

        protected override void Start()
        {
            animator = AnimationManager.GetAnimator(this.gameObject);
        }
    }
}