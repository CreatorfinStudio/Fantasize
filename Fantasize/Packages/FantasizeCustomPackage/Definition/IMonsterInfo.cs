namespace Definition
{
    public interface IMonsterInfo
    {
        public int GetHp();
        public int GetHungry();

        //public float GetMoveSpeed();
        public float GetAttackPower();
        public float GetAttackSpeed();

        public float GetFollowRange();
        public float GetAtackRange();

        public MonterState GetAtackState();
        public MonsterType GetMonsterType();

        public void SetHp(int hp);
        //public void SetMoveSpeed(float moveSpeed);
        public void SetAttackPower(float attackPower);
        public void SetAttackSpeed(float attackSpeed);

        public void SetFollowRange(float rangedView);
        public void SetAtackRange(float forwardView);

        public MonterState SetAtackState(MonterState monterState);
        public MonsterType SetMonsterType(MonsterType monsterType);
        public void SetItemInfo();
    }
}