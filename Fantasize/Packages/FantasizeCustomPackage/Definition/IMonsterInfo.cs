using UnityEngine;

namespace Definition
{
    public interface IMonsterInfo
    {
        public float GetHp();
        public float GetMoveSpeed();
        public float GetAttackPower();
        public float GetAttackSpeed();

        public float GetJumpForce();
        public bool GetCanSeePlayer();
        public float GetFollowSpeed();
        public float GetAtackRange();
        public float GetRushSpeed();
        public bool GetIsCanRush();
        public MonterState GetAtackState();
        public MonsterType GetMonsterType();

        public void SetHp(int hp);
        public void SetMoveSpeed(float moveSpeed);
        public void SetAttackPower(float attackPower);
        public void SetAttackSpeed(float attackSpeed);

        public void SetJumpForce(float jumpForce);
        public void SetCanSeePlayer(bool isCanSee);
        public void SetFollowSpeed(float followSpeed);
        public void SetAtackRange(float rangedView);
        public void SetRushSpeed(float rushSpeed);
        public void SetIsCanRush(bool isCanRush);

        public void SetAtackState(MonterState monterState);
        public void SetMonsterType(MonsterType monsterType);
        public void SetDropItemInfo();
    }
}