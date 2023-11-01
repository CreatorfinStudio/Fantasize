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
        public float attackDistance = 1f; // �÷��̾���� ���� �Ÿ�
        public Animator animator; // �ִϸ����� ������Ʈ ����

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

        // 4�� ���� �̵�
        float elapsedTime = 0f;
        void Roam_Update()
        {
            //if (IsPlayerClose()) //�÷��̾�� ������
            //    Debug.Log("�����");
            //else
            //    Debug.Log("�ִ�");


            if (elapsedTime < 4f)
            {
                float pingPongX = Mathf.PingPong(elapsedTime * imonsterInfo.GetMoveSpeed(), patrolDistance);
                Vector3 newPosition = startPosi + new Vector3(movingRight ? pingPongX : -pingPongX, 0f, 0f);
                transform.position = newPosition;

                //  �̵� ���⿡ ���� y ȸ���� �ʱ�ȭ
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
                // �̵� ���� ����
                movingRight = !movingRight;

                // 1�� ���� ����
                // AnimationManager.BoolAnim(animator, "Walk", false);

                //  yield return new WaitForSeconds(stopDuration);

                // ���� ��ġ���� �ݴ� �������� �̵��ϱ� ���� ���� ��ġ�� ������Ʈ
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
        //public float attackDistance = 1f; // �÷��̾���� ���� �Ÿ�
        //public Animator animator; // �ִϸ����� ������Ʈ ����

        //private Vector3 startPosition;
        //private bool movingRight = true;
        //private bool isAttacking = false;

        //private void Start()
        //{
        //    animator = GetComponent<Animator>();
        //    // �ʱ� ��ġ�� ����
        //    startPosition = transform.position;

        //     //Patrolling �ڷ�ƾ�� ����
        //    AnimationManager.BoolAnim(animator, "Walk", true);
        //    StartCoroutine(PatrollingRoutine());
        //}

        //private void Update()
        //{
        //    // �÷��̾���� �Ÿ� ���
        //    float playerDistance = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);

        //     //�÷��̾�� ����� ������ ����
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
        //            // 4�� ���� �̵�
        //            float elapsedTime = 0f;
        //            while (elapsedTime < 4f)
        //            {
        //                float pingPongX = Mathf.PingPong(elapsedTime * patrolSpeed, patrolDistance);
        //                Vector3 newPosition = startPosition + new Vector3(movingRight ? pingPongX : -pingPongX, 0f, 0f);
        //                transform.position = newPosition;

        //               //  �̵� ���⿡ ���� y ȸ���� �ʱ�ȭ
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

        //            // �̵� ���� ����
        //            movingRight = !movingRight;

        //            // 1�� ���� ����
        //            AnimationManager.BoolAnim(animator, "Walk", false);

        //            yield return new WaitForSeconds(stopDuration);

        //            // ���� ��ġ���� �ݴ� �������� �̵��ϱ� ���� ���� ��ġ�� ������Ʈ
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

        //        // Attack �ִϸ��̼��� ���
        //        animator.SetTrigger("Attack");
        //    }
        //}

        //private void EndAttack()
        //{
        //    // Attack �ִϸ��̼� ���� �� ȣ��Ǵ� �Լ�
        //    AnimationManager.BoolAnim(animator, "Walk", true);
        //    isAttacking = false;

        //    // �÷��̾��� ��ġ ��������
        //    Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        //    // �÷��̾ �ٶ󺸴� ���� ���
        //    Vector3 directionToPlayer = playerTransform.position - transform.position;
        //    directionToPlayer.y = 0f; // Y ������ ������� ����

        //    // �÷��̾ �ٶ󺸵��� ȸ��
        //    transform.rotation = Quaternion.LookRotation(directionToPlayer);

        //    // ���͸� �÷��̾� �������� �̵� (���ϴ� �ӵ��� ����)
        //    Vector3 moveDirection = directionToPlayer.normalized;
        //    Vector3 moveVelocity = moveDirection * patrolSpeed;
        //    transform.position += moveVelocity * Time.deltaTime;
        //}

        //private void OnDrawGizmosSelected()
        //{
        //    // ���� ������ �ð������� ǥ��
        //    Gizmos.color = Color.red;
        //    Gizmos.DrawWireSphere(transform.position, attackDistance);
        //}

    }
}
