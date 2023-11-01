using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;
using Definition;
using System;
using UnityEditor.XR;

namespace Monster
{
    public class SmallMonster : Monster
    {
        private StateMachine<MonterState> fsm;
        private SpriteRenderer spriteRenderer;
        #region Roaming Around
        public float patrolDistance = 5f;
        public float patrolSpeed = 1f;
        public float stopDuration = 1f;
        public float attackDistance = 1f; // 플레이어와의 공격 거리
        public Animator animator; // 애니메이터 컴포넌트 참조

        private Vector3 startPosi;
        private bool movingRight = true;
        #endregion

        private void Awake()
        {
            startPosi = transform.position;

            spriteRenderer = GetComponent<SpriteRenderer>();
            fsm = StateMachine<MonterState>.Initialize(this);
            fsm.ChangeState(MonterState.Roam);
        }

        // 4초 동안 이동
        float elapsedTime = 0f;
        void Roam_Update()
        {
            //if (IsPlayerClose()) //플레이어와 가까우면
            //    Debug.Log("가까움");
            //else
            //    Debug.Log("멀당");


            if (elapsedTime < 4f)
            {
                float pingPongX = Mathf.PingPong(elapsedTime * imonsterInfo.GetMoveSpeed(), patrolDistance);
                Vector3 newPosition = startPosi + new Vector3(movingRight ? pingPongX : -pingPongX, 0f, 0f);
                transform.position = newPosition;

                //  이동 방향에 따라 y 회전값 초기화
                if (movingRight)
                {
                    spriteRenderer.flipX = false;
                }
                else
                {
                    spriteRenderer.flipX = true;
                }

                elapsedTime += Time.deltaTime;
            }
            else
            {
                // 이동 방향 변경
                movingRight = !movingRight;

                // 1초 동안 정지
                // AnimationManager.BoolAnim(animator, "Walk", false);

                //  yield return new WaitForSeconds(stopDuration);

                // 현재 위치에서 반대 방향으로 이동하기 위해 시작 위치를 업데이트
                startPosi = transform.position;
                elapsedTime = 0f;
            }
        }
    
        private bool IsPlayerClose()
        {
            if (playerPosi != null)
            {
                float dis = Vector3.Distance(this.transform.position, playerPosi.position);
                if (dis <= 1f)
                    return true;
            }
            return false;
        }













        //public float patrolDistance = 5f;
        //public float patrolSpeed = 1f;
        //public float stopDuration = 1f;
        //public float attackDistance = 1f; // 플레이어와의 공격 거리
        //public Animator animator; // 애니메이터 컴포넌트 참조

        //private Vector3 startPosition;
        //private bool movingRight = true;
        //private bool isAttacking = false;

        //private void Start()
        //{
        //    animator = GetComponent<Animator>();
        //    // 초기 위치를 저장
        //    startPosition = transform.position;

        //     //Patrolling 코루틴을 시작
        //    AnimationManager.BoolAnim(animator, "Walk", true);
        //    StartCoroutine(PatrollingRoutine());
        //}

        //private void Update()
        //{
        //    // 플레이어와의 거리 계산
        //    float playerDistance = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);

        //     //플레이어와 충분히 가까우면 공격
        //    if (playerDistance < attackDistance && !isAttacking)
        //    {
        //        Attack();
        //    }
        //    else
        //    {
        //        EndAttack();
        //    }
        //}

        //private IEnumerator PatrollingRoutine()
        //{
        //    while (true)
        //    {
        //        if (!isAttacking)
        //        {
        //            // 4초 동안 이동
        //            float elapsedTime = 0f;
        //            while (elapsedTime < 4f)
        //            {
        //                float pingPongX = Mathf.PingPong(elapsedTime * patrolSpeed, patrolDistance);
        //                Vector3 newPosition = startPosition + new Vector3(movingRight ? pingPongX : -pingPongX, 0f, 0f);
        //                transform.position = newPosition;

        //               //  이동 방향에 따라 y 회전값 초기화
        //                if (movingRight)
        //                {
        //                    transform.rotation = Quaternion.identity;
        //                }
        //                else
        //                {
        //                    transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        //                }

        //                elapsedTime += Time.deltaTime;
        //                yield return null;
        //            }

        //            // 이동 방향 변경
        //            movingRight = !movingRight;

        //            // 1초 동안 정지
        //            AnimationManager.BoolAnim(animator, "Walk", false);

        //            yield return new WaitForSeconds(stopDuration);

        //            // 현재 위치에서 반대 방향으로 이동하기 위해 시작 위치를 업데이트
        //            startPosition = transform.position;
        //        }
        //        yield return null;
        //    }
        //}

        //private void Attack()
        //{
        //    if (!isAttacking)
        //    {
        //        isAttacking = true;
        //        AnimationManager.BoolAnim(animator, "Walk", false);

        //        // Attack 애니메이션을 재생
        //        animator.SetTrigger("Attack");
        //    }
        //}

        //private void EndAttack()
        //{
        //    // Attack 애니메이션 종료 후 호출되는 함수
        //    AnimationManager.BoolAnim(animator, "Walk", true);
        //    isAttacking = false;

        //    // 플레이어의 위치 가져오기
        //    Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        //    // 플레이어를 바라보는 방향 계산
        //    Vector3 directionToPlayer = playerTransform.position - transform.position;
        //    directionToPlayer.y = 0f; // Y 방향은 고려하지 않음

        //    // 플레이어를 바라보도록 회전
        //    transform.rotation = Quaternion.LookRotation(directionToPlayer);

        //    // 몬스터를 플레이어 방향으로 이동 (원하는 속도로 조절)
        //    Vector3 moveDirection = directionToPlayer.normalized;
        //    Vector3 moveVelocity = moveDirection * patrolSpeed;
        //    transform.position += moveVelocity * Time.deltaTime;
        //}

        //private void OnDrawGizmosSelected()
        //{
        //    // 공격 범위를 시각적으로 표시
        //    Gizmos.color = Color.red;
        //    Gizmos.DrawWireSphere(transform.position, attackDistance);
        //}

    }
}
