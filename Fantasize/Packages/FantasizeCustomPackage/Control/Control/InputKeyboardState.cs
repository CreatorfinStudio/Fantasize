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
        #region Componentx
        private Rigidbody2D rb;
        private SpriteRenderer spriteRenderer;
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
        //public float doubleClickTimeThreshold = 0.2f; // ���� Ŭ�� ����(��)
        //private KeyCode lastKey = KeyCode.None; // ���������� ���� Ű
        //private float lastKeyClickTime = 0f;

        private bool isDashing = false;
        private float doubleClickTimeThreshold = 0.3f; // ����Ŭ�� ���� (���� ����)

        private KeyCode lastKey = KeyCode.None;
        private float lastKeyClickTime = 0f;

        //private float lastKeyPressTime = 0f;
        //private bool isDashing = false;


        /// <summary>
        /// FSM
        /// </summary>
        private StateMachine<PlayerMove> moveFSM;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            moveFSM = StateMachine<PlayerMove>.Initialize(this);
            iplayerInfo?.SetMoveFSM(this.moveFSM.State);
            rb = GetComponent<Rigidbody2D>();
            moveFSM.ChangeState(PlayerMove.Idle);
        }

        private void Update()
        {
            iplayerInfo?.SetMoveFSM(this.moveFSM.State);

            if (this.moveFSM.State != PlayerMove.Run && !isJump)
            {
                float currentTime = Time.time;

                // ���� �Է� Ű
                KeyCode currentKey = KeyCode.None;

                if (Input.GetKeyDown(KeyCode.LeftArrow))
                    currentKey = KeyCode.LeftArrow;
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                    currentKey = KeyCode.RightArrow;

                // Ű �Է��� üũ�ϰ� �뽬 ���·� ��ȯ
                if (currentKey != KeyCode.None)
                {
                    // ����Ŭ�� ����
                    if (currentKey == lastKey && (currentTime - lastKeyClickTime) <= doubleClickTimeThreshold)
                    {
                        isDashing = true;
                    }
                    else
                    {
                        isDashing = false;
                    }

                    lastKey = currentKey;
                    lastKeyClickTime = currentTime;
                }
                else
                    isDashing = false;
                if (isDashing)
                {
                    Debug.Log("Dash!");
                    //isDashing = false;
                    moveFSM.ChangeState(PlayerMove.Run);
                }
            }
        }

        void Idle_Update()
        {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
                moveFSM.ChangeState(PlayerMove.Walk);          
        }
        /// <summary>
        /// �ȱ�
        /// </summary>
        void Walk_Update()
        {

            h = Input.GetAxis("Horizontal"); // ���� �Է� (A �� D Ű �Ǵ� ȭ��ǥ Ű)    
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    spriteRenderer.flipX = true;
                }
                else
                {
                    spriteRenderer.flipX = false;
                }

                //if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow)) //�հ����� ��������
                //{
                //    moveFSM.ChangeState(PlayerMove.Walk);
                //}

                Move(iplayerInfo.GetWalkSpeed());
                if (Input.GetKey(KeyCode.S) && canJump)
                    moveFSM.ChangeState(PlayerMove.WalkJump);
            }
            else if (iplayerInfo?.GetMoveFSM() != PlayerMove.Run)
            {
                moveFSM.ChangeState(PlayerMove.Idle);
            }
            //if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
            //{
            //    isDashing = false;
            //}
        }

        /// <summary>
        /// ����
        /// </summary>
        void WalkJump_Update()
        {
            float horizontalInput = Input.GetAxis("Horizontal");

            // ĳ���� �̵�
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    spriteRenderer.flipX = true;
                }
                else
                {
                    spriteRenderer.flipX = false;
                }
            }
            rb.velocity = new Vector2(horizontalInput * iplayerInfo.GetWalkSpeed(), rb.velocity.y);

            if (!isJump && canJump) //���� ���� �ƴϸ鼭 ������ �� ���� ����
            {                
                rb.velocity = new Vector3(rb.velocity.x, iplayerInfo.GetJumpForce());
                isJump = true;
            }
            //else
            //{
            //    moveFSM.ChangeState(PlayerMove.Walk);
            //}
        }

        /// <summary>
        /// ��� (�޸���)
        /// </summary>
        void Run_Update()
        {

            if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
            {
                isDashing = false;
                moveFSM.ChangeState(PlayerMove.Walk);
            }
            else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                if (isDashing)
                {
                    if (Input.GetKey(KeyCode.LeftArrow))
                    {
                        spriteRenderer.flipX = true;
                    }
                    else
                    {
                        spriteRenderer.flipX = false;
                    }

                    Move(iplayerInfo.GetRunSpeed());
                    moveFSM.ChangeState(PlayerMove.Run);
                }
            }
            else
            {
                isDashing = false;
                moveFSM.ChangeState(PlayerMove.Walk);
            }
        }

        void Move(float moveSpeed)
        {
            Vector2 moveDirection = new Vector3(h, 0f);
            moveDirection.Normalize();
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
            if (Input.GetKey(KeyCode.S))
                moveFSM.ChangeState(PlayerMove.RunJump);
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
                if(iplayerInfo?.GetMoveFSM() == PlayerMove.WalkJump)
                    moveFSM.ChangeState(PlayerMove.Walk);
                else if(iplayerInfo?.GetMoveFSM() == PlayerMove.RunJump)
                    moveFSM.ChangeState(PlayerMove.Run);
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