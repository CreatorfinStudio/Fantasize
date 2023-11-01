namespace Definition
{
    public interface IMonsterInfo
    {
        public float GetHp();
        public float GetMoveSpeed();
        public float GetAttackPower();
        public float GetAttackSpeed();

        public float GetFollowRange();
        public float GetAtackRange();

        public MonterState GetAtackState();
        public MonsterType GetMonsterType();

        public void SetHp(int hp);
        public void SetMoveSpeed(float moveSpeed);
        public void SetAttackPower(float attackPower);
        public void SetAttackSpeed(float attackSpeed);

        public void SetFollowRange(float rangedView);
        public void SetAtackRange(float rangedView);

        public void SetAtackState(MonterState monterState);
        public void SetMonsterType(MonsterType monsterType);
        public void SetDropItemInfo();
    }
}