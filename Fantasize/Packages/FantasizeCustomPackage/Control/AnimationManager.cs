using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Control
{
    public static class AnimationManager
    {
        public static Animator GetAnimator(GameObject g)
        {
            return g.GetComponent<Animator>();
        }

        public static void BoolAnim(Animator a,string name, bool state) => a.SetBool(name, state);
        public static void TriggerAnim(Animator a,string name) => a.SetTrigger(name);

        /// <summary>
        /// 모든 애니 파라매터 false
        /// </summary>
        public static void AllAnimOff(Animator animator)
        {
            var t = animator.parameters;
            foreach (var parameter in t)
            {
                if (parameter.type == AnimatorControllerParameterType.Bool)
                {
                    animator.SetBool(parameter.name, false);
                }
            }
        }

    }
}