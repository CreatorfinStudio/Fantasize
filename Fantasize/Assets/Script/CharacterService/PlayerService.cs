using Control;
using Definition;
using UnityEngine;
using Item;
using System;
using MonsterLove.StateMachine;

namespace Player
{
    public class PlayerService : MonoBehaviour , IPlayerInfo
    {
        public PlayerInfo playerInfo;

        //FSM떄매 임시 주석
       // private bool isCanUseItem => DefinitionManager.Instance.iItemProcessing.IsUseItem();
        private Action useItem = () => DefinitionManager.Instance.iItemProcessing.UseItem();

        private void Start()
        {
            SetComponent();
        }

        private void Update()
        {
            ////슬롯 아이템 사용가능상태
            //if (isCanUseItem && Input.GetKeyDown(KeyCode.Space))
            //{
            //    SetItemInfo();
            //    useItem.Invoke();
            //}
        }

        private void SetComponent()
        {
            this.gameObject.AddComponent<InputKeyboardState>();
         //   this.gameObject.AddComponent<MouseRotation>();
            //this.gameObject.AddComponent<ComboAttack>();
            //this.gameObject.AddComponent<LongClick>();
        }

        #region PlayerInfo Data Interface
        public int GetHp() => playerInfo.Hp;
        public int GetHungry() => playerInfo.Hungry;

        public float GetWalkSpeed() => playerInfo.WalkSpeed;
        public float GetAttackPower() => playerInfo.AttackPower;
        public float GetAttackSpeed() => playerInfo.AttackSpeed;
        // 0905 기획에서 마우스 회전 관련 삭제됨
        //public float GetRotationSpeed() => playerInfo.RotationSpeed;

        public float GetRunSpeed() => playerInfo.RunSpeed;
        public float GetRunJumpForce() => playerInfo.RunJumpForce;
        public float GetDashSpeed() => playerInfo.DashSpeed;

        public float GetRangedView() => playerInfo.RangedView;
        public float GetForwardView() => playerInfo.ForwardView;

        public int GetMaxHP() => playerInfo.MaxHP;
        public int GetMaxHungry() => playerInfo.MaxHungry;     

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
        PlayerState IPlayerInfo.GetMoveFSM()
        {
            return playerInfo.MOVEFSM;
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