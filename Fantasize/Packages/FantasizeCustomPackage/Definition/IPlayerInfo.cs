namespace Definition
{
    public interface IPlayerInfo
    {
        ////////////////// GET //////////////////
        public float GetHp();
        public float GetMaxHP();

        public int GetHaveCoin();

        public bool GetIsDashing();
        public bool GetIsJumping();
        public bool GetIsCanJump();
        public float GetDashDirection();
        public AttackType GetAttackType();
        public float GetCastingSpeed();

        public float GetMoveSpeed();
        public float GetAttackPower();
        public float GetSpecialAttackPower();
        public float GetAttackSpeed();

        // 0905 기획에서 마우스 회전 관련 삭제됨
        // public float GetRotationSpeed();

        /// <summary>
        /// 임시 추가
        /// </summary>
        /// <returns></returns>
        public float GetJumpForce();
        public float GetDashSpeed();
        public float GetRunJumpForce();
        public float GetRangedView();
        public float GetForwardView();

        ////////////////// SET //////////////////
        public void SetHp(float hp);
        public void SetMaxHP(float maxHP);

        public void SetHaveCoin(int haveCoin);
        public void SetIsDashing(bool isDashing);
        public void SetIsJumping(bool isJumping);
        public void SetIsCanJump(bool isCanJump);
        public void SetDashDirection(float dashDirection);
        public void SetAttackType(AttackType attackType);
        public void SetCastingSpeed(float castingSpeed);

        public void SetMoveSpeed(float moveSpeed);
        public void SetAttackPower(float attackPower);
        public void SetSpecialAttackPower(float specialAttackPower);
        public void SetAttackSpeed(float attackSpeed);

        /////////////////// 합/곱연산에 따른 다른 처리 ///////////////////
        
        /// <summary>
        /// 합연산
        /// </summary>
        /// <param name="itemInfo"></param>
        public void SetAddItemStatsToPlayer(ItemInfo itemInfo, bool isShop);
        /// <summary>
        /// 곱연산
        /// </summary>
        /// <param name="itemInfo"></param>
        public void SetMultiplicationItemStatsToPlayer(ItemInfo itemInfo);


        /// <summary>
        /// 임시 주가
        /// </summary>
        /// <param name="jumpForce"></param>
        public void SetJumpForce(float jumpForce);
        public void SetDashSpeed(float dashSpeed);

        public void SetRunJumpForce(float runJumpForce);
        public void SetRangedView(float rangedView);
        public void SetForwardView(float forwardView);


    }
}