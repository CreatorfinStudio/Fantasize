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
        /// 점프 중인지 여부
        /// </summary>
        bool isJump = false;
        /// <summary>
        ///지면을 밟아 재점프가 가능한지 여부
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
        //public float doubleClickTimeThreshold = 0.2f; // 더블 클릭 간격(초)
        //private KeyCode lastKey = KeyCode.None; // 마지막으로 눌린 키
        //private float lastKeyClickTime = 0f;

        private bool isDashing = false;
        private float doubleClickTimeThreshold = 0.3f; // 더블클릭 간격 (조절 가능)

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

                // 현재 입력 키
                KeyCode currentKey = KeyCode.None;

                if (Input.GetKeyDown(KeyCode.LeftArrow))
                    currentKey = KeyCode.LeftArrow;
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                    currentKey = KeyCode.RightArrow;

                // 키 입력을 체크하고 대쉬 상태로 전환
                if (currentKey != KeyCode.None)
                {
                    // 더블클릭 판정
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
        /// 걷기
        /// </summary>
        void Walk_Update()
        {

            h = Input.GetAxis("Horizontal"); // 수평 입력 (A 및 D 키 또는 화살표 키)    
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

                //if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow)) //손가락이 떨어지면
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
        /// 점프
        /// </summary>
        void WalkJump_Update()
        {
            float horizontalInput = Input.GetAxis("Horizontal");

            // 캐릭터 이동
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

            if (!isJump && canJump) //점프 중이 아니면서 지면일 때 점프 가능
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
        /// 대시 (달리기)
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
        /// 벽과 구분이 아직 안되어있기 때문에, 벽을 밟고 연속점프가 가능. 나중에 벽과 바닥의 구분을 지어줄 것
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider != null) //바닥에 뭔가 닿았다면 재점프 가능
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
                //    Debug.Log("--비었음");
            }
        }
    }
}