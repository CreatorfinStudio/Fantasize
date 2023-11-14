using Definition;
using MonsterLove.StateMachine;
using System.Collections;
using UnityEngine;

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
        /// <summary>
        /// 대시 중인지 여부
        /// </summary>
        bool isDashing = false;
        #endregion
        #region State floats

        #endregion
        private float h;

        private float doubleClickTimeThreshold = 0.3f; // 더블클릭 간격 (조절 가능)

        private KeyCode lastKey = KeyCode.None;
        private float lastKeyClickTime = 0f;

        /// <summary>
        /// FSM
        /// </summary>
        private StateMachine<PlayerState> moveFSM;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            moveFSM = StateMachine<PlayerState>.Initialize(this);
            iplayerInfo?.SetMoveFSM(this.moveFSM.State);
            rb = GetComponent<Rigidbody2D>();
            moveFSM.ChangeState(PlayerState.Idle);
        }

        private void Update()
        {
            iplayerInfo?.SetMoveFSM(this.moveFSM.State);
            Debug.Log(this.moveFSM.State);
            h = Input.GetAxis("Horizontal");
            if (h < 0)
            {
                spriteRenderer.flipX = true;
            }
            else if (h > 0)
            {
                spriteRenderer.flipX = false;
            }

            #region 대쉬 판정
            //if (this.moveFSM.State != PlayerState.Run && !isJump)
            if (!isJump)
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
                        isDashDone = false;
                        moveFSM.ChangeState(PlayerState.Dash);
                    }
                    else
                    {
                        isDashing = false;
                      //  moveFSM.ChangeState(PlayerState.Run);
                    }

                    lastKey = currentKey;
                    lastKeyClickTime = currentTime;
                }
                //else
                //{
                //    if (this.moveFSM.State != PlayerState.RunJump)
                //        isDashing = false;
                //}
                //if (isDashing && this.moveFSM.State != PlayerState.RunJump)
                //{
                //    moveFSM.ChangeState(PlayerState.Run);
                //}
            }
            #endregion
        }

        void Idle_Update()
        {
            if (h != 0 && (this.moveFSM.State != PlayerState.Dash) && isDashDone)
            {
                Debug.Log("////Run");
                moveFSM.ChangeState(PlayerState.Run);
            }
            else if (Input.GetKey(KeyCode.Space))
                moveFSM.ChangeState(PlayerState.Jump);
            if (Input.GetKey(KeyCode.D))
                moveFSM.ChangeState(PlayerState.Attack);
        }
        #region 걷기 (삭제)
        /*
        /// <summary>
        /// 걷기
        /// </summary>
        void Walk_Update()
        {
            h = Input.GetAxis("Horizontal");  
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                Move(iplayerInfo.GetWalkSpeed());
            }
            else if (!isDashing)
            {
                moveFSM.ChangeState(PlayerMove.Idle);
            }
        }

        /// <summary>
        /// 걷기 점프
        /// </summary>
        void WalkJump_Update()
        {
            h = Input.GetAxis("Horizontal");

            rb.velocity = new Vector2(h * iplayerInfo.GetWalkSpeed(), rb.velocity.y);

            if (!isJump && canJump) //점프 중이 아니면서 지면일 때 점프 가능
            {                
                rb.velocity = new Vector3(rb.velocity.x, iplayerInfo.GetJumpForce());
                isJump = true;
            }
        }
        */
        #endregion

        #region 달리기

        /// <summary>
        /// 달리기
        /// </summary>
        void Run_Update()
        {
            if (Input.GetKeyUp(KeyCode.LeftArrow) && Input.GetKeyUp(KeyCode.RightArrow))
            {
                moveFSM.ChangeState(PlayerState.RunStop);
            }
            else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                Move(iplayerInfo.GetRunSpeed());
            }
            else
            {
                moveFSM.ChangeState(PlayerState.RunStop);
            }
        }
        /// <summary>
        /// 달리기 멈춤
        /// </summary>
        void RunStop_Enter()
        {
            StartCoroutine(WaitRunStopMotion());
        }
        /// <summary>
        /// 달리기 멈춤 대기 시간
        /// </summary>
        /// <returns></returns>        
        float runStopWaitTime = .1f;
        IEnumerator WaitRunStopMotion()
        {
            yield return new WaitForSeconds(runStopWaitTime);
            moveFSM.ChangeState(PlayerState.Idle);
        }
        /// <summary>
        /// 달리기 점프
        /// </summary>
        void RunJump_Update()
        {
            rb.velocity = new Vector2(h * iplayerInfo.GetRunSpeed(), rb.velocity.y);

            if (!isJump && canJump) //점프 중이 아니면서 지면일 때 점프 가능
            {
                rb.velocity = new Vector3(rb.velocity.x, iplayerInfo.GetJumpForce());
                isJump = true;
            }
        }

        void Move(float moveSpeed)
        {
            Vector2 moveDirection = new Vector3(h, 0f);
            moveDirection.Normalize();
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
            if (Input.GetKey(KeyCode.Space) && canJump)
            {
                //걷는중 점프 , 달리기중 점프가 나뉘었던 부분 삭제
                //if(iplayerInfo?.GetMoveFSM() == PlayerMove.Walk)
                //    moveFSM.ChangeState(PlayerMove.WalkJump);
                //else
                moveFSM.ChangeState(PlayerState.RunJump);
            }
            //if(Input.GetKey(KeyCode.D))

        }
        #endregion

        #region 대시

        bool isDashDone = true;
        void Dash_Enter()
        {      
            StartCoroutine(Dash());         
        }

        /// <summary>
        /// 대시 후 달리기로 전환되기까지 대기 시간
        /// </summary>
        float dashTime = .4f;
        IEnumerator Dash()
        {
            float dashDirection = Input.GetKeyDown(KeyCode.LeftArrow) ? -1f : 1f;
            rb.velocity = new Vector2(iplayerInfo.GetDashSpeed() * dashDirection, rb.velocity.y);
            yield return new WaitForSeconds(dashTime);
            isDashDone = true;
            moveFSM.ChangeState(PlayerState.Run);
        }

        #endregion

        #region 점프
        void Jump_Enter()
        {
            rb.velocity = new Vector3(rb.velocity.x, iplayerInfo.GetJumpForce());
        }
        #endregion

        #region 공격

        /// <summary>
        /// 현재 임시 단일공격. 나중에 콤보로 변경되거나 해서 코드 수정 될 수 있음
        /// </summary>
        void Attack_Enter()
        {
            StartCoroutine (WaitAttack());
        }

        /// <summary>
        /// 임시 공격 후 대기시간 (애니메이션 끝날때까지 대기 -> Idle 전환)
        /// </summary>
        /// <returns></returns>
        IEnumerator WaitAttack()
        {
            yield return new WaitForSeconds(.08f);
                moveFSM.ChangeState(PlayerState.Idle);
        }
        #endregion

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
                if (iplayerInfo?.GetMoveFSM() == PlayerState.RunJump)
                    moveFSM.ChangeState(PlayerState.Run);
                else
                    moveFSM.ChangeState(PlayerState.Idle);
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