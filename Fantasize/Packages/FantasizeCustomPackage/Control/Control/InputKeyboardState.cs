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
        public float doubleClickTimeThreshold = 0.3f; // 더블 클릭 간격(초)
        private KeyCode lastKey = KeyCode.None; // 마지막으로 눌린 키
        private float lastKeyClickTime = 0f;


        private void Awake()
        {
            fsm = StateMachine<PlayerMove>.Initialize(this);
            rb = GetComponent<Rigidbody2D>();
            fsm.ChangeState(PlayerMove.Walk);
        }

        /// <summary>
        /// 걷기
        /// </summary>
        void Walk_Update()
        {
            h = Input.GetAxis("Horizontal"); // 수평 입력 (A 및 D 키 또는 화살표 키)    
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow)) //손가락이 떨어지면
                {
                    fsm.ChangeState(PlayerMove.Walk);
                }

                Move(iplayerInfo.GetWalkSpeed());
                KeyCode currentKey = KeyCode.None;

                // 대쉬 여부
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
        /// 점프
        /// </summary>
        void Jump_Update()
        {
            if (!isJump && canJump) //점프 중이 아니면서 지면일 때 점프 가능
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
        /// 대시 (달리기)
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
        /// 벽과 구분이 아직 안되어있기 때문에, 벽을 밟고 연속점프가 가능. 나중에 벽과 바닥의 구분을 지어줄 것
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider != null) //바닥에 뭔가 닿았다면 재점프 가능
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
                //    Debug.Log("--비었음");
            }
        }
    }
}