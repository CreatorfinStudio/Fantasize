using Definition;
using System.Collections;
using UnityEngine;
using MonsterLove.StateMachine;

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
        #region State 
        /// <summary>
        /// 직전 상태 저장용
        /// </summary>
        private PlayerState beforeState = PlayerState.None;
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
         //   iplayerInfo?.SetMoveFSM(this.moveFSM.State);
            rb = GetComponent<Rigidbody2D>();
            moveFSM.ChangeState(PlayerState.Idle);
        }

        private float dPressTime = 0f;
        private void Update()
        {
            //Debug.Log(beforeState);
           // iplayerInfo?.SetMoveFSM(this.moveFSM.State);
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
                        isDashDone = false;
                        moveFSM.ChangeState(PlayerState.Dash);
                    }
                    else
                    {
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

            #region 공격
            if (Input.GetKeyDown(KeyCode.D))
            {
                dPressTime = Time.time;
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                float elapsedTime = Time.time - dPressTime;
                if (elapsedTime <= .5f)
                {
                    //Debug.Log("어택 진입");
                    moveFSM.ChangeState(PlayerState.Attack);
                }
                else
                {
                    moveFSM.ChangeState(PlayerState.SpecialAttack);
                }
            }
            #endregion

        }

        /// <summary>
        /// 여기 나중에 로직 정리좀해야 함
        /// </summary>
        void Idle_Update()
        {
            if (h != 0 && (this.moveFSM.State != PlayerState.Dash) && isDashDone)
            {
                moveFSM.ChangeState(PlayerState.Run);
            }
            else if (Input.GetKeyDown(KeyCode.Space))
                moveFSM.ChangeState(PlayerState.Jump);
            else if (Input.GetKeyDown(KeyCode.D))
                beforeState = PlayerState.Idle;
            else if (Input.GetKeyDown(KeyCode.S))
            {
                beforeState = PlayerState.Idle;
                moveFSM.ChangeState(PlayerState.Block);
            }
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
        /// 조작이 완전히 종료되었을때만 RunStop으로 전환하기 위한 변수
        /// </summary>
        private float inputTimeout = 0.4f; // 입력 타임아웃 시간
        private float lastInputTime; // 마지막 입력 시간
        /// <summary>
        /// 달리기
        /// </summary>
        void Run_Update()
        {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                lastInputTime = Time.time;
                if (Input.GetKeyDown(KeyCode.D))
                    beforeState = PlayerState.Run;
                Move(iplayerInfo.GetRunSpeed());
            }
            else if (Time.time - lastInputTime >= inputTimeout && !Input.anyKey)
            {
                moveFSM.ChangeState(PlayerState.RunStop);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                beforeState = PlayerState.Run;
                moveFSM.ChangeState(PlayerState.Block);
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
        void Move(float moveSpeed)
        {
            Vector2 moveDirection = new Vector3(h, 0f);
            moveDirection.Normalize();
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
            if (Input.GetKey(KeyCode.Space) && canJump)
            {
                moveFSM.ChangeState(PlayerState.Jump);
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
        float dashTime = .6f;
        IEnumerator Dash()
        {
            float dashDirection = Input.GetKeyDown(KeyCode.LeftArrow) ? -1f : 1f;
            rb.velocity = new Vector2(iplayerInfo.GetDashSpeed() * dashDirection, rb.velocity.y);
            yield return new WaitForSeconds(dashTime);
            rb.velocity = Vector2.zero;
            isDashDone = true;
            moveFSM.ChangeState(PlayerState.Run);
        }

        #endregion

        #region 점프
        /// <summary>
        /// 점프
        /// </summary>
        void Jump_Update()
        {
            rb.velocity = new Vector2(h * iplayerInfo.GetRunSpeed(), rb.velocity.y);

            if (!isJump && canJump) //점프 중이 아니면서 지면일 때 점프 가능
            {
                rb.velocity = new Vector3(rb.velocity.x, iplayerInfo.GetJumpForce());
                isJump = true;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                beforeState = PlayerState.None;
            }
        }
        #endregion

        #region 공격
        /// <summary>
        /// 현재 일반 단일공격. 나중에 콤보로 변경되거나 해서 코드 수정 될 수 있음
        /// 점프중에 시전될 시, 공중 공격으로 전환된다.
        /// </summary>
        void Attack_Enter()
        {
            StartCoroutine(WaitAttack(CoroutineWait.wait05));
        }

        /// <summary>
        /// 특수 공격
        /// </summary>
        void SpecialAttack_Enter()
        {
            StartCoroutine(WaitAttack(CoroutineWait.wait05));
        }

        /// <summary>
        /// 임시 공격 후 대기시간 (애니메이션 끝날때까지 대기 -> 원래 상태로 다시 전환)
        /// </summary>
        /// <returns></returns>
        IEnumerator WaitAttack(WaitForSeconds time)
        {
            yield return time;
            if(beforeState != PlayerState.None)            
                moveFSM.ChangeState(beforeState);
            else
            {                // 이전 상태가 None이면 기본 상태로 변경
                moveFSM.ChangeState(PlayerState.Idle);
            }
        }
        #endregion

        #region 방어
        private bool blockSuccess = false;

        void Block_Enter()
        {
            StartCoroutine(WaitBlockMotion());
        }
        IEnumerator WaitBlockMotion()
        {
            yield return new WaitForSeconds(.6f);

            if (blockSuccess)
                moveFSM.ChangeState(PlayerState.BlockSuccess);
            else
                moveFSM.ChangeState(PlayerState.BlockFail);
        }
        void BlockSuccess_Enter()
        {
            StartCoroutine(WaitBlockMotion(.37f));
        }
        void BlockFail_Enter()
        {
            StartCoroutine(WaitBlockMotion(.37f));
        }
        IEnumerator WaitBlockMotion(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            
            if(beforeState == PlayerState.Idle || beforeState == PlayerState.Run)
                moveFSM.ChangeState(beforeState);
            else
                moveFSM.ChangeState(PlayerState.Run);
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
                //if (iplayerInfo?.GetMoveFSM() == PlayerState.Jump)
                //    moveFSM.ChangeState(PlayerState.Run);
                //else
                    moveFSM.ChangeState(PlayerState.Idle);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            switch (collision.tag)
            {
                case "Bullet":
                    //if (iplayerInfo?.GetMoveFSM() == PlayerState.Block)
                    //    blockSuccess = true;
                    //else
                    //    blockSuccess = false;
                    break;
            }
        }

    }
}