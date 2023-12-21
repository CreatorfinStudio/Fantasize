using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;
using Definition;
using System;
using Codice.Client.BaseCommands;

namespace Monster
{
    public class SmallMonster : Monster
    {
        private SpriteRenderer spriteRenderer;
        private bool movingRight = true;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        private void Update()
        {
            movingRight = !movingRight;
            //  이동 방향에 따라 y 회전값 초기화
            spriteRenderer.flipX = !movingRight;
        }

        #region FSM
        /*
        #region Roaming Around (FSM)
        public float patrolDistance = 5f;
        public float patrolSpeed = 1f;
        public float stopDuration = 1f;
        public float attackDistance = 1f; // 플레이어와의 공격 거리
        public Animator animator; // 애니메이터 컴포넌트 참조

        private Vector3 startPosi;
        private bool movingRight = true;
        #endregion

        #region 로머 몬스터
        /*
        // 4초 동안 이동
        float elapsedTime = 0f;
        bool stopRoam = false;
        /// <summary>
        /// 로머 몬스터일 경우 사용
        /// </summary>
        void Roam_Update()
        {
            if (IsPlayerClose()) //플레이어와 가까우면
            {
                stopRoam = true;
                Vector3 targetPosition = new Vector3(playerPosi.position.x, transform.position.y, playerPosi.position.z);
                transform.position = Vector3.Lerp(transform.position, targetPosition, imonsterInfo.GetFollowSpeed() * Time.deltaTime);

                spriteRenderer.flipX = playerPosi.position.x < transform.position.x;
            }
            else
                stopRoam = false;

            if (!stopRoam)
            {
                if (elapsedTime < 4f)
                {
                    float pingPongX = Mathf.PingPong(elapsedTime * imonsterInfo.GetMoveSpeed(), patrolDistance);
                    Vector3 newPosition = startPosi + new Vector3(movingRight ? pingPongX : -pingPongX, 0f, 0f);
                    transform.position = newPosition;

                    elapsedTime += Time.deltaTime;
                }
                else
                {
                    // 이동 방향 변경
                    movingRight = !movingRight;
                    //  이동 방향에 따라 y 회전값 초기화
                    spriteRenderer.flipX = !movingRight;

                    // 현재 위치에서 반대 방향으로 이동하기 위해 시작 위치를 업데이트
                    startPosi = transform.position;
                    elapsedTime = 0f;
                }
            }
        }

        #endregion

        private void Idle_Enter()
        {
            
        }

        /// <summary>
        /// 플레이어와 거리 판정
        /// </summary>
        /// <returns></returns>
        private bool IsPlayerClose()
        {
            if (playerPosi != null)
            {
                float dis = Vector3.Distance(this.transform.position, playerPosi.position);
                if (dis <= imonsterInfo.GetFollowRange())
                    return true;
            }
            return false;
        }

        */
        #endregion
    }
}
