using BehaviorDesigner.Runtime;
using Definition;
using System;
using UnityEngine;

namespace Player
{
    public class PlayerService : MonoBehaviour, IPlayerInfo
    {
        private float h;
        private Rigidbody2D rb;

        public PlayerInfo playerInfo;

        //FSM떄매 임시 주석
        // private bool isCanUseItem => DefinitionManager.Instance.iItemProcessing.IsUseItem();
        private Action useItem = () => DefinitionManager.Instance.iItemProcessing.UseItem();

        //특수공격 판정용
        public float dPressTime = 0f;

        [Header("대시 유지 시간")]
        [SerializeField]
        private float playDashTime = 0.8f;
        //대시 재시전 가능여부 판정용
        private BehaviorTree behaviorTree;
        private bool isDashSet = false;

        public static bool canUseRunAnim = false;

        private void Start()
        {
            dPressTime = 0f;
            behaviorTree = GetComponent<BehaviorTree>();
            rb = GetComponent<Rigidbody2D>();

            SetHp(GetMaxHP());
        }

        private void Update()
        {
            CheckFlipX();
            SetCurrAttackType();
            SetCanBeDash();
        }

        /// <summary>
        /// 실시간 스프라이트 방향체크
        /// </summary>
        private void CheckFlipX()
        {
            if (!GetIsDashing())
            {
                h = Input.GetAxis("Horizontal");
                Vector3 currentScale = transform.localScale;

                if (h < 0)
                {
                    if (currentScale.x < 0)
                    {
                        currentScale.x *= -1;
                        transform.localScale = currentScale;
                    }
                }
                else if (h > 0)
                {
                    if (currentScale.x > 0)
                    {
                        currentScale.x *= -1;
                        transform.localScale = currentScale;
                    }
                }
            }
        }
        private void SetCurrAttackType()
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                dPressTime = Time.time;
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                float elapsedTime = Time.time - dPressTime;
                if (elapsedTime <= .5f)
                {
                    if (GetIsJumping())
                        SetAttackType(AttackType.AirAttack);
                    else
                        SetAttackType(AttackType.Attack);
                }
                else
                {
                    SetAttackType(AttackType.SpecialAttack);
                }
            }
        }

        /// <summary>
        /// 대시 기능 보완 (대시 중 대시시전 X , 대시 종료 판정 및 후처리)
        /// </summary>
        private void SetCanBeDash()
        {
            if (GetIsDashing())
            {
                var startTime = behaviorTree.GetVariable("dashStartTime");
                if (!isDashSet)
                {
                    startTime?.SetValue(Time.time);
                    isDashSet = true;
                }
                if (Time.time - (float)startTime?.GetValue() >= playDashTime || Input.GetKeyDown(KeyCode.Space))
                {
                    rb.velocity = Vector2.zero;
                    SetDashDirection(0);
                    SetIsDashing(false);
                }
                rb.velocity = new Vector2(10f * DefinitionManager.Instance.iplayerInfo.GetDashDirection(), rb.velocity.y);
            }
            else
                isDashSet = false;
        }

        #region PlayerInfo Data Interface

        /////////////////////// GET ///////////////////////     
        public float GetHp() => playerInfo.Hp;
        public float GetMaxHP() => playerInfo.MaxHP;

        public bool GetIsDashing() => playerInfo.IsDashing;
        public bool GetIsJumping() => playerInfo.IsJumping;
        public bool GetIsCanJump() => playerInfo.IsCanJump;
        public float GetDashDirection() => playerInfo.DashDirection;
        public AttackType GetAttackType() => playerInfo.AttackType;

        public float GetWalkSpeed() => playerInfo.WalkSpeed;
        public float GetAttackPower() => playerInfo.AttackPower;
        public float GetSpecialAttackPower() => playerInfo.SpecialAttackPower;
        public float GetAttackSpeed() => playerInfo.AttackSpeed;
        public float GetRunSpeed() => playerInfo.RunSpeed;
        public float GetRunJumpForce() => playerInfo.RunJumpForce;
        public float GetDashSpeed() => playerInfo.DashSpeed;

        public float GetRangedView() => playerInfo.RangedView;
        public float GetForwardView() => playerInfo.ForwardView;

        /////////////////////// SET ///////////////////////     
        public void SetHp(float hp)
        {
            playerInfo.Hp += hp;
            if (playerInfo.Hp > playerInfo.MaxHP)
                playerInfo.Hp = playerInfo.MaxHP;
        }
        public void SetIsDashing(bool isDashing) => playerInfo.IsDashing = isDashing;
        public void SetIsJumping(bool isJumping) => playerInfo.IsJumping = isJumping;
        public void SetIsCanJump(bool isCanJump) => playerInfo.IsCanJump = isCanJump;
        public void SetDashDirection(float dashDirection) => playerInfo.DashDirection = dashDirection;
        public void SetAttackType(AttackType attackType) => playerInfo.AttackType = attackType;

        public void SetWalkSpeed(float moveSpeed) => playerInfo.WalkSpeed += moveSpeed;
        public void SetAttackPower(float attackPower) => playerInfo.AttackPower += attackPower;
        public void SetSpecialAttackPower(float specialAttackPower) => playerInfo.SpecialAttackPower += specialAttackPower;
        public void SetAttackSpeed(float attackSpeed) => playerInfo.AttackSpeed += attackSpeed;

        public void SetRunSpeed(float runSpeed) => playerInfo.RunSpeed += runSpeed;
        public void SetRunJumpForce(float jumpForce) => playerInfo.JumpForce += jumpForce;
        public void SetRangedView(float rangedView) => playerInfo.RangedView += rangedView;
        public void SetForwardView(float forwardView) => playerInfo.ForwardView += forwardView;
        public void SetMaxHP(float maxHP) => playerInfo.MaxHP += maxHP;

        /// <summary>
        /// 임시 추가
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public float GetJumpForce() => playerInfo.JumpForce;

        public void SetJumpForce(float jumpForce)
        {
            playerInfo.JumpForce = jumpForce;
        }
        public void SetDashSpeed(float dashSpeed)
        {
            playerInfo.DashSpeed = dashSpeed;
        }

        #endregion

    }
}