namespace Definition
{
    public interface IPlayerInfo
    {
        ////////////////// GET //////////////////
        public float GetHp();
        public float GetMaxHP();

        public bool GetIsDashing();
        public bool GetIsJumping();
        public bool GetIsCanJump();
        public float GetDashDirection();
        public AttackType GetAttackType();

        public float GetWalkSpeed();
        public float GetAttackPower();
        public float GetSpecialAttackPower();
        public float GetAttackSpeed();

        // 0905 ��ȹ���� ���콺 ȸ�� ���� ������
        // public float GetRotationSpeed();

        /// <summary>
        /// �ӽ� �߰�
        /// </summary>
        /// <returns></returns>
        public float GetJumpForce();
        public float GetDashSpeed();
        public float GetRunSpeed();
        public float GetRunJumpForce();
        public float GetRangedView();
        public float GetForwardView();

        ////////////////// SET //////////////////
        public void SetHp(float hp);
        public void SetMaxHP(float maxHP);

        public void SetIsDashing(bool isDashing);
        public void SetIsJumping(bool isJumping);
        public void SetIsCanJump(bool isCanJump);
        public void SetDashDirection(float dashDirection);
        public void SetAttackType(AttackType attackType);

        //   public void SetMoveFSM(PlayerState moveFsm);
        public void SetWalkSpeed(float moveSpeed);
        public void SetAttackPower(float attackPower);
        public void SetSpecialAttackPower(float specialAttackPower);
        public void SetAttackSpeed(float attackSpeed);

        /// <summary>
        /// �ӽ� �ְ�
        /// </summary>
        /// <param name="jumpForce"></param>
        public void SetJumpForce(float jumpForce);
        public void SetDashSpeed(float dashSpeed);

        public void SetRunSpeed(float runSpeed);
        public void SetRunJumpForce(float runJumpForce);
        public void SetRangedView(float rangedView);
        public void SetForwardView(float forwardView);

    }
}