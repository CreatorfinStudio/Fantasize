using Control;
using Definition;
using UnityEngine;
using Item;
using System;
using MonsterLove.StateMachine;
using PlayerBehavior;
using BehaviorDesigner.Runtime;

namespace Player
{
    public class PlayerService : MonoBehaviour , IPlayerInfo
    {
        private float h;
        private SpriteRenderer spriteRenderer;
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
            spriteRenderer = GetComponent<SpriteRenderer>();
            behaviorTree = GetComponent<BehaviorTree>();
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            CheckFlipX();
            SetCurrAttackType();
            SetCanBeDash();
        }

        /// <summary>
        /// 실시간 스프라이트 flipX 방향체크
        /// </summary>
        private void CheckFlipX()
        {
            if (!DefinitionManager.Instance.iplayerInfo.GetIsDashing())
            { 
                h = Input.GetAxis("Horizontal");

                if (h < 0)
                {
                    spriteRenderer.flipX = true;
                }
                else if (h > 0)
                {
                    spriteRenderer.flipX = false;
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
                    DefinitionManager.Instance.iplayerInfo.SetAttackType(AttackType.Attack);
                }
                else
                {
                    DefinitionManager.Instance.iplayerInfo.SetAttackType(AttackType.SpecialAttack);
                }
            }          
        }

        /// <summary>
        /// 대시 기능 보완 (대시 중 대시시전 X , 대시 종료 판정 및 후처리)
        /// </summary>
        private void SetCanBeDash()
        {
            if (DefinitionManager.Instance.iplayerInfo.GetIsDashing())
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
                    DefinitionManager.Instance.iplayerInfo.SetDashDirection(0);
                    DefinitionManager.Instance.iplayerInfo.SetIsDashing(false);
                }
                rb.velocity = new Vector2(10f * DefinitionManager.Instance.iplayerInfo.GetDashDirection(), rb.velocity.y);
            }
            else
                isDashSet = false;
        }

        #region PlayerInfo Data Interface
        public bool GetIsDashing() => playerInfo.IsDashing;
        public bool GetIsJumping() => playerInfo.IsJumping;
        public bool GetIsCanJump() => playerInfo.IsCanJump;
        public float GetDashDirection() => playerInfo.DashDirection;
        public AttackType GetAttackType() => playerInfo.AttackType;

        PlayerState IPlayerInfo.GetMoveFSM()
        {
            return playerInfo.MOVEFSM;
        }
        public int GetHp() => playerInfo.Hp;
        public int GetHungry() => playerInfo.Hungry;

        public float GetWalkSpeed() => playerInfo.WalkSpeed;
        public float GetAttackPower() => playerInfo.AttackPower;
        public float GetAttackSpeed() => playerInfo.AttackSpeed;
        public float GetRunSpeed() => playerInfo.RunSpeed;
        public float GetRunJumpForce() => playerInfo.RunJumpForce;
        public float GetDashSpeed() => playerInfo.DashSpeed;

        public float GetRangedView() => playerInfo.RangedView;
        public float GetForwardView() => playerInfo.ForwardView;

        public int GetMaxHP() => playerInfo.MaxHP;
        public int GetMaxHungry() => playerInfo.MaxHungry;
        public float GetAirAttackPower() => playerInfo.AirAttackPower;


        public void SetIsDashing(bool isDashing) => playerInfo.IsDashing = isDashing;
        public void SetIsJumping(bool isJumping) => playerInfo.IsJumping = isJumping;
        public void SetIsCanJump(bool isCanJump) => playerInfo.IsCanJump = isCanJump;
        public void SetDashDirection(float dashDirection) => playerInfo.DashDirection = dashDirection;
        public void SetAttackType(AttackType attackType) => playerInfo.AttackType = attackType;

        public void SetHp(int hp)
        {
            playerInfo.Hp += hp;
            if(playerInfo.Hp > playerInfo.maxHP)
                playerInfo.Hp = playerInfo.maxHP;
        }
        public void SetHungry(int hungry)
        {
            playerInfo.Hungry += hungry;
            if(playerInfo.Hungry > playerInfo.MaxHungry)
                playerInfo.Hungry = playerInfo.MaxHungry;
        }

        public void SetWalkSpeed(float moveSpeed) => playerInfo.WalkSpeed += moveSpeed;
        public void SetAttackPower(float attackPower) => playerInfo.AttackPower += attackPower;
        public void SetAttackSpeed(float attackSpeed) => playerInfo.AttackSpeed += attackSpeed;
        // 0905 기획에서 마우스 회전 관련 삭제됨
        //public void SetRotationSpeed(float rotationSpeed) => playerInfo.RotationSpeed += rotationSpeed;

        public void SetRunSpeed(float runSpeed) => playerInfo.RunSpeed += runSpeed;
        public void SetRunJumpForce(float jumpForce) => playerInfo.JumpForce += jumpForce;
        public void SetRangedView(float rangedView) => playerInfo.RangedView += rangedView;
        public void SetForwardView(float forwardView) => playerInfo.ForwardView += forwardView;
        public void SetMaxHungry(int maxHungry) => playerInfo.MaxHungry += maxHungry;
        public void SetMaxHP(int maxHP) => playerInfo.MaxHP += maxHP;
        public void SetItemInfo()
        {
            if(Item.Item.itemSlotInfo != null)
            {
                SetHp(Item.Item.itemSlotInfo.HP);
                SetHungry(Item.Item.itemSlotInfo.Hungry);
                SetWalkSpeed(Item.Item.itemSlotInfo.MoveSpeed);
                SetAttackPower(Item.Item.itemSlotInfo.ApplicationTime);
                SetAttackSpeed(Item.Item.itemSlotInfo.AttackSpeed);
                //SetRotationSpeed(Item.Item.itemSlotInfo.);
                SetRangedView(Item.Item.itemSlotInfo.RangedView);
                SetForwardView(Item.Item.itemSlotInfo.ForwardView);
               // SetMaxHungry(Item.Item.itemSlotInfo.maxh);
               // SetMaxHP(Item.Item.itemSlotInfo.max);
            }
        }
        public void SetItemInfo(Definition.InteractionItem item)
        {
            if(item != null)
            {
                SetHp(item.HP);
                SetHungry(item.Hungry);
                SetWalkSpeed(item.MoveSpeed);
                SetAttackPower(item.ApplicationTime);
                SetAttackSpeed(item.AttackSpeed);
                SetRangedView(item.RangedView);
                SetForwardView(item.ForwardView);
            }
        }    
        public void SetItemInfo(Definition.FieldItem item)
        {
            SetHp(item.HP);
            SetHungry(item.Hungry);
        }
        public void SetAirAttackPower()
        {
        }
        public void SetMoveFSM(PlayerState moveFsm)
        {
            playerInfo.MOVEFSM = moveFsm;
        }

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