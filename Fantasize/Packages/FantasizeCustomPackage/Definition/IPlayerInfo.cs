using MonsterLove.StateMachine;

namespace Definition
{
    public interface IPlayerInfo
    {
        ////////////////// GET //////////////////
        public PlayerState GetMoveFSM();
        public int GetHp();
        public int GetHungry();

        public float GetWalkSpeed();
        public float GetAttackPower();
        public float GetAttackSpeed();

        // 0905 ��ȹ���� ���콺 ȸ�� ���� ������
        // public float GetRotationSpeed();

        /// <summary>
        /// �ӽ� �߰�
        /// </summary>
        /// <returns></returns>
        public float GetJumpForce();

        public float GetRunSpeed();
        public float GetRunJumpForce();
        public float GetRangedView();
        public float GetForwardView();

        public int GetMaxHP();
        public int GetMaxHungry();

        ////////////////// SET //////////////////
        public void SetMoveFSM(PlayerState moveFsm);

        public void SetHp(int hp);
        public void SetHungry(int hungry);

        public void SetWalkSpeed(float moveSpeed);
        public void SetAttackPower(float attackPower);
        public void SetAttackSpeed(float attackSpeed);

        // 0905 ��ȹ���� ���콺 ȸ�� ���� ������
        //public void SetRotationSpeed(float rotationSpeed);

        /// <summary>
        /// �ӽ� �ְ�
        /// </summary>
        /// <param name="jumpForce"></param>
        public void SetJumpForce(float jumpForce);
        public void SetRunSpeed(float runSpeed);
        public void SetRunJumpForce(float runJumpForce);
        public void SetRangedView(float rangedView);
        public void SetForwardView(float forwardView);
        public void SetMaxHungry(int maxHungry);
        public void SetMaxHP(int maxHP);

        /// <summary>
        /// ���� ������ ����
        /// </summary>
        public void SetItemInfo();
        public void SetItemInfo(InteractionItem item);
        public void SetItemInfo(FieldItem item);
    }
}