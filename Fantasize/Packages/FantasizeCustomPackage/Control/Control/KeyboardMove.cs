//using Definition;
//using System.Collections;
//using System.Runtime.InteropServices.WindowsRuntime;
//using UnityEngine;

//namespace Control
//{
//    public class KeyboardMove : Controller
//    {
//        private float h;
//        private float v;

//        private Animator animator;

//        private bool walk = false;
//        private bool backWalk = false;
//        private bool jump = false;
//        private bool isCanJump = false;
//        //private bool sit = false; //����

//        /// <summary>
//        /// �̵� ��
//        /// </summary>
//        private Vector3 movement;

//        /// <summary>
//        /// Dash
//        /// </summary>
//        public float doubleClickTimeThreshold = 0.3f; // ���� Ŭ�� ����(��)
//        private KeyCode lastKey = KeyCode.None; // ���������� ���� Ű
//        private float lastKeyClickTime = 0f;

//        private bool isDash = false;
//        private bool isStoping = false; //��� ���� �ִϸ��̼� ����������


//        private Rigidbody rb;


//        //��� ������ ���� Start , Anim ������ �����ؾ� �� �ʿ�����
//        protected override void Start()
//        {
//            base.Start();
//            animator = AnimationManager.GetAnimator(this.gameObject);
//            rb = GetComponent<Rigidbody>();
//            StartCoroutine(FSMInput());
//            StartCoroutine(FSMMove());
//            StartCoroutine(FSMAnim());
//        }

//        private IEnumerator FSMInput()
//        {
//            while (true)
//            {
//                yield return null;

//                h = Input.GetAxis("Horizontal");
//                v = Input.GetAxis("Vertical");

//                if (!isStoping)
//                {
//                    if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
//                    {
//                        KeyCode currentKey = KeyCode.None;

//                        // �뽬 ����
//                        if (Input.GetKeyDown(KeyCode.LeftArrow)) currentKey = KeyCode.LeftArrow;
//                        else if (Input.GetKeyDown(KeyCode.RightArrow)) currentKey = KeyCode.RightArrow;

//                        if (currentKey != KeyCode.None)
//                        {
//                            if (currentKey == lastKey && (Time.time - lastKeyClickTime) <= doubleClickTimeThreshold)
//                            {
//                                isDash = true;
//                            }
//                            else
//                            {
//                                lastKey = currentKey;
//                                lastKeyClickTime = Time.time;
//                                isDash = false;
//                            }
//                        }
//                        ///

//                        if (Input.GetKey(KeyCode.DownArrow)) //����
//                        {
//                            moveState = PlayerMove.SitWalk;
//                        }
//                        else if (Input.GetKey(KeyCode.S) && isCanJump && !jump) //����
//                        {
//                            moveState = PlayerMove.Jump;
//                        }
//                        else if (isDash) //�뽬
//                        {
//                            moveState = PlayerMove.Run;
//                        }
//                        else
//                        {
//                            if (Input.GetKey(KeyCode.LeftArrow))
//                            {
//                                //���� ���� ���¸� Walk ��ȯ
//                                if (isCanJump && !jump)
//                                    moveState = PlayerMove.BackWalk;
//                            }
//                            else
//                            {
//                                //���� ���� ���¸� Walk ��ȯ
//                                if (isCanJump && !jump)
//                                    moveState = PlayerMove.Walk;
//                            }
//                        }
//                    }
//                    else
//                    {
//                        if (Input.GetKeyDown(KeyCode.DownArrow))
//                            moveState = PlayerMove.Sit;
//                        else if (Input.GetKeyDown(KeyCode.S) && isCanJump)
//                            moveState = PlayerMove.Jump;
//                        else
//                        {
//                            //���� ���� ���¸� Idle ��ȯ
//                            if (isCanJump && !jump)
//                            {
//                                if (!isDash && !isStoping) //�뽬���̾����� ����
//                                    moveState = PlayerMove.Idle;
//                                else
//                                {
//                                    isDash = false;
//                                    moveState = PlayerMove.RunStop;
//                                }
//                            }
//                        }
//                    }
//                }                
//            }
//        }

//        /// <summary>
//        /// ���� ANim Issue : �ݶ��̴��� �ִϸ��̼Ǻ��� ���� ��Ƽ� ���߿��� �������Ǵ°�ó�� ����
//        /// -> ���߿� Anim ��ü�ϸ鼭 �ش� ���� Ȯ���� �� ��
//        /// </summary>
//        /// <returns></returns>
//        private IEnumerator FSMAnim()
//        {
//            Quaternion rotation;

//            while (true)
//            {
//                yield return null;

//                switch (moveState)
//                {
//                    case PlayerMove.Idle:
//                        AnimationManager.BoolAnim(animator, "Walk", false);
//                        AnimationManager.BoolAnim(animator, "Jump", false);
//                        FalseWalk();
//                        jump = false;
//                        break;
//                    case PlayerMove.Walk:
//                        AnimationManager.BoolAnim(animator, "Jump", false);
//                        walk = true;
//                        jump = false;
//                        rotation = this.gameObject.transform.rotation;
//                        rotation.y = 0f;
//                        this.gameObject.transform.rotation = rotation;
//                        break;
//                    case PlayerMove.BackWalk:
//                        AnimationManager.BoolAnim(animator, "Jump", false);
//                        backWalk = true;
//                        jump = false;
//                        rotation = this.gameObject.transform.rotation;
//                        rotation.y = -180f;
//                        this.gameObject.transform.rotation = rotation;
//                        break;
//                    case PlayerMove.Run:
//                        AnimationManager.BoolAnim(animator, "Jump", false);
//                        FalseWalk();
//                        jump = false;
//                        break;
//                    case PlayerMove.RunStop:
//                        AnimationManager.BoolAnim(animator, "Run", false);
//                        AnimationManager.BoolAnim(animator, "RunStop", true);
//                        isStoping = true;
//                        yield return CoroutineWait.wait15;
//                        AnimationManager.BoolAnim(animator, "RunStop", false);
//                        yield return CoroutineWait.wait05;
//                        isStoping = false;
//                        isDash = false;
//                        moveState = PlayerMove.Idle;
//                        break;
//                    case PlayerMove.Sit:
//                        FalseWalk();
//                        break;
//                    case PlayerMove.SitWalk:
//                        FalseWalk();

//                        break;
//                    case PlayerMove.Jump:
//                        jump = true;
//                        FalseWalk();
//                        break;
//                }
//            }

//            void FalseWalk()
//            {
//                walk = false;
//                backWalk = false;
//            }
//        }
        
//        //public bool IsDashStopping()
//        //{
//        //    return isDashStoping = true;
//        //}
//        //public bool IsDoneDashStopping()
//        //{
//        //    return isDashStoping = false;
//        //}
//        private IEnumerator FSMMove()
//        {
//            while (true)
//            {
//                yield return null;

//                if (walk || backWalk)
//                {
//                    movement = new Vector3(0, 0.0f, h);
//                    rb.velocity = movement * iplayerInfo.GetWalkSpeed();
//                    AnimationManager.BoolAnim(animator, "Walk", true);

//                    if (jump && isCanJump)
//                    {
//                        AnimationManager.BoolAnim(animator, "Walk", false);
//                        AnimationManager.BoolAnim(animator, "Jump", true);
//                        rb.velocity = new Vector3(rb.velocity.x, iplayerInfo.GetJumpForce(), rb.velocity.z);
//                    }
//                }
//                else                     
//                {
//                    if (jump && isCanJump)
//                    {
//                        AnimationManager.BoolAnim(animator, "Jump", true);
//                        rb.velocity = new Vector3(rb.velocity.x, iplayerInfo.GetJumpForce(), rb.velocity.z);
//                    }
//                    if (isDash)
//                    {
//                        AnimationManager.BoolAnim(animator, "Walk", false);
//                        AnimationManager.BoolAnim(animator, "Run", true);

//                        movement = new Vector3(0, 0.0f, h);
//                        rb.velocity = movement * iplayerInfo.GetRunSpeed();
//                    }
//                }
      
//            }

//            //    Debug.Log("Jump : " + jump + "  //  IsCanJump : " + isCanJump + "  // MoveState : " + moveState);
//        }
       

//        private void OnCollisionStay(Collision collision)
//        {
//            if (collision.gameObject.tag == "Ground")
//            {
//                isCanJump = true;
//            }
//        }

//        private void OnCollisionExit(Collision collision)
//        {
//            if (collision.gameObject.tag == "Ground")
//                isCanJump = false;
//        }

//        private void OnCollisionEnter(Collision collision)
//        {
//            if (collision.gameObject.tag == "Ground")
//            {
//                isCanJump = true;
//                jump = false;
//            }
//        }

//    }
//}