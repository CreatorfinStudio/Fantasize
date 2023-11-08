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
            iplayerInfo?.SetMoveFSM(this.moveFSM.State);
            rb = GetComponent<Rigidbody2D>();
            moveFSM.ChangeState(PlayerState.Idle);
        }

        private void Update()
        {
            iplayerInfo?.SetMoveFSM(this.moveFSM.State);
            h = Input.GetAxis("Horizontal");
            if (h < 0)
            {
                spriteRenderer.flipX = true;
            }
            else if (h > 0)
            {
                spriteRenderer.flipX = false;
            }

            #region �뽬 �޸��� ����
            //if (this.moveFSM.State != PlayerMove.Run && !isJump)
            //{
            //    float currentTime = Time.time;

            //    // ���� �Է� Ű
            //    KeyCode currentKey = KeyCode.None;

            //    if (Input.GetKeyDown(KeyCode.LeftArrow))
            //        currentKey = KeyCode.LeftArrow;
            //    else if (Input.GetKeyDown(KeyCode.RightArrow))
            //        currentKey = KeyCode.RightArrow;

            //    // Ű �Է��� üũ�ϰ� �뽬 ���·� ��ȯ
            //    if (currentKey != KeyCode.None)
            //    {
            //        // ����Ŭ�� ����
            //        if (currentKey == lastKey && (currentTime - lastKeyClickTime) <= doubleClickTimeThreshold)
            //        {
            //            isDashing = true;
            //        }
            //        else
            //        {
            //            isDashing = false;
            //            moveFSM.ChangeState(PlayerMove.Walk);
            //        }

            //        lastKey = currentKey;
            //        lastKeyClickTime = currentTime;
            //    }
            //    else
            //    {
            //        if(this.moveFSM.State != PlayerMove.RunJump)
            //            isDashing = false;
            //    }
            //    if (isDashing && this.moveFSM.State != PlayerMove.RunJump)
            //    {
            //        moveFSM.ChangeState(PlayerMove.Run);
            //    }
            // }
            #endregion
        }

        void Idle_Update()
        {
            if (h != 0)
                moveFSM.ChangeState(PlayerState.Run);
            else if (Input.GetKey(KeyCode.Space))
                moveFSM.ChangeState(PlayerState.Jump);
            if (Input.GetKey(KeyCode.D))
                moveFSM.ChangeState(PlayerState.Attack);
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
        /// �޸���
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
        float waitTime = .1f;
        IEnumerator WaitRunStopMotion()
        {
            yield return new WaitForSeconds(waitTime);
            moveFSM.ChangeState(PlayerState.Idle);
        }
        /// <summary>
        /// �޸��� ����
        /// </summary>
        void RunJump_Update()
        {
            rb.velocity = new Vector2(h * iplayerInfo.GetRunSpeed(), rb.velocity.y);

            if (!isJump && canJump) //���� ���� �ƴϸ鼭 ������ �� ���� ����
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
                //�ȴ��� ���� , �޸����� ������ �������� �κ� ����
                //if(iplayerInfo?.GetMoveFSM() == PlayerMove.Walk)
                //    moveFSM.ChangeState(PlayerMove.WalkJump);
                //else
                moveFSM.ChangeState(PlayerState.RunJump);
            }
            //if(Input.GetKey(KeyCode.D))

        }
        #endregion

        #region ����
        void Jump_Enter()
        {
            rb.velocity = new Vector3(rb.velocity.x, iplayerInfo.GetJumpForce());
        }
        #endregion

        #region ����

        /// <summary>
        /// ���� �ӽ� ���ϰ���. ���߿� �޺��� ����ǰų� �ؼ� �ڵ� ���� �� �� ����
        /// </summary>
        void Attack_Enter()
        {
            StartCoroutine (WaitAttack());
        }

        /// <summary>
        /// �ӽ� ���� �� ���ð� (�ִϸ��̼� ���������� ��� -> Idle ��ȯ)
        /// </summary>
        /// <returns></returns>
        IEnumerator WaitAttack()
        {
            yield return new WaitForSeconds(.08f);
                moveFSM.ChangeState(PlayerState.Idle);
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
                //    Debug.Log("--�����");
            }
        }
    }
}