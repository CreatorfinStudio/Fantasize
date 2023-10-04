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
        private Dictionary<PlayerMove, Action> statesDic = new Dictionary<PlayerMove, Action>();
        private void Awake()
        {
            animator = GetComponent<Animator>();
            StartCoroutine(SetAnimation());
        }

        /// <summary>
        /// 애니메이션 딕셔너리 초기화
        /// </summary>
        private void SetStateEnumDic()
        {
            statesDic.Clear();
            statesDic.Add(PlayerMove.Idle, () =>
            {
                AnimationManager.BoolAnim(animator, "Walk", false);
                AnimationManager.BoolAnim(animator, "WalkJump", false);
                AnimationManager.BoolAnim(animator, "Run", false);
                AnimationManager.BoolAnim(animator, "RunJump", false);
            });
            statesDic.Add(PlayerMove.Walk, () => AnimationManager.BoolAnim(animator, "Walk", true));
            statesDic.Add(PlayerMove.Run, () => AnimationManager.BoolAnim(animator, "Run", true));
            statesDic.Add(PlayerMove.WalkJump, () => AnimationManager.BoolAnim(animator, "WalkJump", true));
            statesDic.Add(PlayerMove.RunJump, () => AnimationManager.BoolAnim(animator, "RunJump", true));
        }
        IEnumerator SetAnimation()
        {
            SetStateEnumDic();
            while (true)
            {
                yield return null;

                statesDic[PlayerMove.Idle]();

                switch (iplayerInfo?.GetMoveFSM())
                {
                    case PlayerMove.Idle:
                        if (statesDic.ContainsKey(PlayerMove.Idle))
                            statesDic[PlayerMove.Idle]();
                        break;
                    case PlayerMove.Walk:
                        if (statesDic.ContainsKey(PlayerMove.Walk))
                            statesDic[PlayerMove.Walk]();
                        break;
                    case PlayerMove.WalkJump:
                        if (statesDic.ContainsKey(PlayerMove.WalkJump))
                            statesDic[PlayerMove.WalkJump]();
                        break;
                    case PlayerMove.RunJump:
                        if (statesDic.ContainsKey(PlayerMove.RunJump))
                            statesDic[PlayerMove.RunJump]();
                        break;
                    case PlayerMove.Run:
                        if (statesDic.ContainsKey(PlayerMove.Run))
                            statesDic[PlayerMove.Run]();
                        break;


                }
            }

            void SetOFFAnim()
            {

            }
        }

    }
}