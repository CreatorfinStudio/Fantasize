using MonsterLove.StateMachine;
using UnityEngine;

namespace Definition
{
    public interface IPlayerInfo
    {
        ////////////////// GET //////////////////
        public bool GetIsDashing();
        public bool GetIsJumping();
        public bool GetIsCanJump();
        public float GetDashDirection();
        public AttackType GetAttackType();

        public PlayerState GetMoveFSM();
        public float GetAirAttackPower();
        public int GetHp();
        public int GetHungry();

        public float GetWalkSpeed();
        public float GetAttackPower();
        public float GetAttackSpeed();

        // 0905 기획에서 마우스 회전 관련 삭제됨
        // public float GetRotationSpeed();

        /// <summary>
        /// 임시 추가
        /// </summary>
        /// <returns></returns>
        public float GetJumpForce();
        public float GetDashSpeed();
        public float GetRunSpeed();
        public float GetRunJumpForce();
        public float GetRangedView();
        public float GetForwardView();

        public int GetMaxHP();
        public int GetMaxHungry();

        ////////////////// SET //////////////////
        public void SetIsDashing(bool isDashing);
        public void SetIsJumping(bool isJumping);
        public void SetIsCanJump(bool isCanJump);
        public void SetDashDirection(float dashDirection);
        public void SetAttackType(AttackType attackType);

        public void SetMoveFSM(PlayerState moveFsm);
        public void SetAirAttackPower();


        public void SetHp(int hp);
        public void SetHungry(int hungry);

        public void SetWalkSpeed(float moveSpeed);
        public void SetAttackPower(float attackPower);
        public void SetAttackSpeed(float attackSpeed);

        // 0905 기획에서 마우스 회전 관련 삭제됨
        //public void SetRotationSpeed(float rotationSpeed);

        /// <summary>
        /// 임시 주가
        /// </summary>
        /// <param name="jumpForce"></param>
        public void SetJumpForce(float jumpForce);
        public void SetDashSpeed(float dashSpeed);

        public void SetRunSpeed(float runSpeed);
        public void SetRunJumpForce(float runJumpForce);
        public void SetRangedView(float rangedView);
        public void SetForwardView(float forwardView);
        public void SetMaxHungry(int maxHungry);
        public void SetMaxHP(int maxHP);

    }
}