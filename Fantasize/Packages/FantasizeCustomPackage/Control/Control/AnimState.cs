using Definition;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Control
{
    public class AnimState : Controller
    {
        private Animator animator;  
        private Dictionary<PlayerState, Action> statesDic = new Dictionary<PlayerState, Action>();
        private void Awake()
        {
            animator = GetComponent<Animator>();
            StartCoroutine(SetPlayerAnimation());
        }

        /// <summary>
        /// 애니메이션 딕셔너리 초기화
        /// </summary>
        private void SetStateEnumDic()
        {
            statesDic.Clear();
            statesDic.Add(PlayerState.Idle, () =>
            {
             //   AnimationManager.BoolAnim(animator, "Walk", false);
             //   AnimationManager.BoolAnim(animator, "WalkJump", false);
                AnimationManager.BoolAnim(animator, "Run", false);
                AnimationManager.BoolAnim(animator, "RunJump", false);
                AnimationManager.BoolAnim(animator, "Jump", false);
                AnimationManager.BoolAnim(animator, "Attack", false);
            });
            //  statesDic.Add(PlayerMove.Walk, () => AnimationManager.BoolAnim(animator, "Walk", true));
            //  statesDic.Add(PlayerMove.WalkJump, () => AnimationManager.BoolAnim(animator, "WalkJump", true));
            statesDic.Add(PlayerState.Run, () => AnimationManager.BoolAnim(animator, "Run", true));         
            statesDic.Add(PlayerState.RunStop, () => AnimationManager.BoolAnim(animator, "Run", false));         
            statesDic.Add(PlayerState.RunJump, () => AnimationManager.BoolAnim(animator, "RunJump", true));
            statesDic.Add(PlayerState.Jump, () => AnimationManager.BoolAnim(animator, "Jump", true));
            statesDic.Add(PlayerState.Attack, () => AnimationManager.BoolAnim(animator, "Attack", true));
        }
        IEnumerator SetPlayerAnimation()
        {
            SetStateEnumDic();
            while (true)
            {
                yield return null;

                statesDic[PlayerState.Idle]();

                switch (iplayerInfo?.GetMoveFSM())
                {
                    case PlayerState.Idle:
                        if (statesDic.ContainsKey(PlayerState.Idle))
                            statesDic[PlayerState.Idle]();
                        break;
                    //case PlayerMove.Walk:
                    //    if (statesDic.ContainsKey(PlayerMove.Walk))
                    //        statesDic[PlayerMove.Walk]();
                    //    break;
                    //case PlayerMove.WalkJump:
                    //    if (statesDic.ContainsKey(PlayerMove.WalkJump))
                    //        statesDic[PlayerMove.WalkJump]();
                    //   break;   
                    case PlayerState.Run:
                        if (statesDic.ContainsKey(PlayerState.Run))
                            statesDic[PlayerState.Run]();
                        break;
                    case PlayerState.RunStop:
                        if (statesDic.ContainsKey(PlayerState.RunStop))
                            statesDic[PlayerState.RunStop]();
                        break;
                    case PlayerState.RunJump:
                        if (statesDic.ContainsKey(PlayerState.RunJump))
                            statesDic[PlayerState.RunJump]();
                        break;       
                    case PlayerState.Jump:
                        if (statesDic.ContainsKey(PlayerState.Jump))
                            statesDic[PlayerState.Jump]();
                        break;
                    case PlayerState.Attack:
                        if (statesDic.ContainsKey(PlayerState.Attack))
                            statesDic[PlayerState.Attack]();
                        break;
                }
            }

            void SetOFFAnim()
            {

            }
        }

    }
}