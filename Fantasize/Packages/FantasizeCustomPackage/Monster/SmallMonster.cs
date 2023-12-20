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
            //  �̵� ���⿡ ���� y ȸ���� �ʱ�ȭ
            spriteRenderer.flipX = !movingRight;
        }

        #region FSM
        /*
        #region Roaming Around (FSM)
        public float patrolDistance = 5f;
        public float patrolSpeed = 1f;
        public float stopDuration = 1f;
        public float attackDistance = 1f; // �÷��̾���� ���� �Ÿ�
        public Animator animator; // �ִϸ����� ������Ʈ ����

        private Vector3 startPosi;
        private bool movingRight = true;
        #endregion

        #region �θ� ����
        /*
        // 4�� ���� �̵�
        float elapsedTime = 0f;
        bool stopRoam = false;
        /// <summary>
        /// �θ� ������ ��� ���
        /// </summary>
        void Roam_Update()
        {
            if (IsPlayerClose()) //�÷��̾�� ������
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
                    // �̵� ���� ����
                    movingRight = !movingRight;
                    //  �̵� ���⿡ ���� y ȸ���� �ʱ�ȭ
                    spriteRenderer.flipX = !movingRight;

                    // ���� ��ġ���� �ݴ� �������� �̵��ϱ� ���� ���� ��ġ�� ������Ʈ
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
        /// �÷��̾�� �Ÿ� ����
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
