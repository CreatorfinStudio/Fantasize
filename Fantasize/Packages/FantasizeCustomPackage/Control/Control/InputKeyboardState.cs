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
        /// ���� ������ ����
        /// </summary>
        bool isJump = false;
        /// <summary>
        ///������ ��� �������� �������� ����
        /// </summary>
        bool canJump = false;
        #endregion
        #region State 
        /// <summary>
        /// ���� ���� �����
        /// </summary>
        private PlayerState beforeState = PlayerState.None;
        #endregion
        private float h;

        private float doubleClickTimeThreshold = 0.3f; // ����Ŭ�� ���� (���� ����)

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

            #region �뽬 ����
            //if (this.moveFSM.State != PlayerState.Run && !isJump)
            if (!isJump)
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

            #region ����
            if (Input.GetKeyDown(KeyCode.D))
            {
                dPressTime = Time.time;
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                float elapsedTime = Time.time - dPressTime;
                if (elapsedTime <= .5f)
                {
                    //Debug.Log("���� ����");
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
        /// ���� ���߿� ���� �������ؾ� ��
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
        #region �ȱ� (����)
        /*
        /// <summary>
        /// �ȱ�
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
        /// �ȱ� ����
        /// </summary>
        void WalkJump_Update()
        {
            h = Input.GetAxis("Horizontal");

            rb.velocity = new Vector2(h * iplayerInfo.GetWalkSpeed(), rb.velocity.y);

            if (!isJump && canJump) //���� ���� �ƴϸ鼭 ������ �� ���� ����
            {                
                rb.velocity = new Vector3(rb.velocity.x, iplayerInfo.GetJumpForce());
                isJump = true;
            }
        }
        */
        #endregion

        #region �޸���

        /// <summary>
        /// ������ ������ ����Ǿ������� RunStop���� ��ȯ�ϱ� ���� ����
        /// </summary>
        private float inputTimeout = 0.4f; // �Է� Ÿ�Ӿƿ� �ð�
        private float lastInputTime; // ������ �Է� �ð�
        /// <summary>
        /// �޸���
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
        /// �޸��� ����
        /// </summary>
        void RunStop_Enter()
        {
            StartCoroutine(WaitRunStopMotion());
        }
        /// <summary>
        /// �޸��� ���� ��� �ð�
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

        #region ���

        bool isDashDone = true;
        void Dash_Enter()
        {
            StartCoroutine(Dash());
        }

        /// <summary>
        /// ��� �� �޸���� ��ȯ�Ǳ���� ��� �ð�
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

        #region ����
        /// <summary>
        /// ����
        /// </summary>
        void Jump_Update()
        {
            rb.velocity = new Vector2(h * iplayerInfo.GetRunSpeed(), rb.velocity.y);

            if (!isJump && canJump) //���� ���� �ƴϸ鼭 ������ �� ���� ����
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

        #region ����
        /// <summary>
        /// ���� �Ϲ� ���ϰ���. ���߿� �޺��� ����ǰų� �ؼ� �ڵ� ���� �� �� ����
        /// �����߿� ������ ��, ���� �������� ��ȯ�ȴ�.
        /// </summary>
        void Attack_Enter()
        {
            StartCoroutine(WaitAttack(CoroutineWait.wait05));
        }

        /// <summary>
        /// Ư�� ����
        /// </summary>
        void SpecialAttack_Enter()
        {
            StartCoroutine(WaitAttack(CoroutineWait.wait05));
        }

        /// <summary>
        /// �ӽ� ���� �� ���ð� (�ִϸ��̼� ���������� ��� -> ���� ���·� �ٽ� ��ȯ)
        /// </summary>
        /// <returns></returns>
        IEnumerator WaitAttack(WaitForSeconds time)
        {
            yield return time;
            if(beforeState != PlayerState.None)            
                moveFSM.ChangeState(beforeState);
            else
            {                // ���� ���°� None�̸� �⺻ ���·� ����
                moveFSM.ChangeState(PlayerState.Idle);
            }
        }
        #endregion

        #region ���
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
        /// ���� ������ ���� �ȵǾ��ֱ� ������, ���� ��� ���������� ����. ���߿� ���� �ٴ��� ������ ������ ��
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider != null) //�ٴڿ� ���� ��Ҵٸ� ������ ����
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