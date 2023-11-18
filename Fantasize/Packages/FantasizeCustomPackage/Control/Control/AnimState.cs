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
                AnimationManager.BoolAnim(animator, "Run", false);
                AnimationManager.BoolAnim(animator, "RunJump", false);
                AnimationManager.BoolAnim(animator, "Dash", false);
                AnimationManager.BoolAnim(animator, "Jump", false);
                AnimationManager.BoolAnim(animator, "Attack", false);
                AnimationManager.BoolAnim(animator, "SpecialAttack", false);
                AnimationManager.BoolAnim(animator, "Block", false);
                AnimationManager.BoolAnim(animator, "BlockSuccess", false);
                AnimationManager.BoolAnim(animator, "BlockFail", false);
            });
            statesDic.Add(PlayerState.Run, () => AnimationManager.BoolAnim(animator, "Run", true));         
            statesDic.Add(PlayerState.RunStop, () => AnimationManager.BoolAnim(animator, "Run", false));         
            statesDic.Add(PlayerState.RunJump, () => AnimationManager.BoolAnim(animator, "RunJump", true));
            statesDic.Add(PlayerState.Dash, () => AnimationManager.BoolAnim(animator, "Dash", true));
            statesDic.Add(PlayerState.Jump, () => AnimationManager.BoolAnim(animator, "Jump", true));         
            statesDic.Add(PlayerState.Attack, () => AnimationManager.BoolAnim(animator, "Attack", true));
            statesDic.Add(PlayerState.SpecialAttack, () => AnimationManager.BoolAnim(animator, "SpecialAttack", true));
            statesDic.Add(PlayerState.Block, () => AnimationManager.BoolAnim(animator, "Block", true));
            statesDic.Add(PlayerState.BlockSuccess, () => AnimationManager.BoolAnim(animator, "BlockSuccess", true));
            statesDic.Add(PlayerState.BlockFail, () => AnimationManager.BoolAnim(animator, "BlockFail", true));
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
                    case PlayerState.Dash:
                        if (statesDic.ContainsKey(PlayerState.Dash))
                            statesDic[PlayerState.Dash]();
                        break;
                    case PlayerState.Jump:
                        if (statesDic.ContainsKey(PlayerState.Jump))
                            statesDic[PlayerState.Jump]();
                        break;      
                    case PlayerState.Attack:
                        if (statesDic.ContainsKey(PlayerState.Attack))
                            statesDic[PlayerState.Attack]();
                        break;    
                    case PlayerState.SpecialAttack:
                        if (statesDic.ContainsKey(PlayerState.SpecialAttack))
                            statesDic[PlayerState.SpecialAttack]();
                        break;         
                    case PlayerState.Block:
                        if (statesDic.ContainsKey(PlayerState.Block))
                            statesDic[PlayerState.Block]();
                        break;        
                    case PlayerState.BlockSuccess:
                        if (statesDic.ContainsKey(PlayerState.BlockSuccess))
                            statesDic[PlayerState.BlockSuccess]();
                        break;                  
                    case PlayerState.BlockFail:
                        if (statesDic.ContainsKey(PlayerState.BlockFail))
                            statesDic[PlayerState.BlockFail]();
                        break;
                }
            }
        }

    }
}