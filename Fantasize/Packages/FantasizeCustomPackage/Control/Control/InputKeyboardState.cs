using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;
using Definition;
using System;

namespace Control
{
    public class InputKeyboardState : Controller
    {       
        StateMachine<PlayerMove> fsm;

        #region Componentx
        private Rigidbody2D rb;
        private Animator animator;

        #endregion
        #region State Bools
        /// <summary>
        /// ���� ������ ����
        /// </summary>
        bool isJump = false;
        /// <summary>
        ///������ ��� �������� �������� ����
        /// </summary>
        bool canJump = false; 
        #endregion
        #region State floats

        #endregion
        private float h;
        private Vector3 movement;

        /// <summary>
        /// Dash
        /// </summary>
        public float doubleClickTimeThreshold = 0.3f; // ���� Ŭ�� ����(��)
        private KeyCode lastKey = KeyCode.None; // ���������� ���� Ű
        private float lastKeyClickTime = 0f;


        private void Awake()
        {
            fsm = StateMachine<PlayerMove>.Initialize(this);
            rb = GetComponent<Rigidbody2D>();
            fsm.ChangeState(PlayerMove.Walk);
        }

        /// <summary>
        /// �ȱ�
        /// </summary>
        void Walk_Update()
        {
            h = Input.GetAxis("Horizontal"); // ���� �Է� (A �� D Ű �Ǵ� ȭ��ǥ Ű)    
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow)) //�հ����� ��������
                {
                    fsm.ChangeState(PlayerMove.Walk);
                }

                Move(iplayerInfo.GetWalkSpeed());
                KeyCode currentKey = KeyCode.None;

                // �뽬 ����
                if (Input.GetKeyDown(KeyCode.LeftArrow)) currentKey = KeyCode.LeftArrow;
                else if (Input.GetKeyDown(KeyCode.RightArrow)) currentKey = KeyCode.RightArrow;

                if (currentKey != KeyCode.None)
                {
                    if (currentKey == lastKey && (Time.time - lastKeyClickTime) <= doubleClickTimeThreshold)
                    {
                        fsm.ChangeState(PlayerMove.Run);
                    }
                    else
                    {
                        lastKey = currentKey;
                        lastKeyClickTime = Time.time;
                    }
                }
            }
            else if (Input.GetKey(KeyCode.S))
                fsm.ChangeState(PlayerMove.Jump);
        }
       
        /// <summary>
        /// ����
        /// </summary>
        void Jump_Update()
        {
            if (!isJump && canJump) //���� ���� �ƴϸ鼭 ������ �� ���� ����
            {
                rb.velocity = new Vector3(rb.velocity.x, iplayerInfo.GetJumpForce());
                isJump = true;
            }
            else
            {
                fsm.ChangeState(PlayerMove.Walk);
            }
        }

        /// <summary>
        /// ��� (�޸���)
        /// </summary>
        void Run_Update()
        {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                Move(iplayerInfo.GetRunSpeed());
            }
            else
            {
                fsm.ChangeState(PlayerMove.Walk);
            }
        }

        void Move(float moveSpeed)
        {
            Vector2 moveDirection = new Vector3(h, 0f);
            moveDirection.Normalize();
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
            if (Input.GetKey(KeyCode.S))
                fsm.ChangeState(PlayerMove.Jump);
        }
       
        /// <summary>
        /// ���� ������ ���� �ȵǾ��ֱ� ������, ���� ��� ���������� ����. ���߿� ���� �ٴ��� ������ ������ ��
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider != null) //�ٴڿ� ���� ��Ҵٸ� ������ ����
            {
                canJump = true;
                isJump = false;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.tag == "ItemBox")
            {

                //var anim = collision.gameObject.GetComponent<Animator>();
                //if (anim != null)
                //    anim.SetTrigger("Open");
                //else
                //    Debug.Log("--�����");
            }
        }
    }
}