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
        public bool GetIsCanAttack();
        public bool GetIsCollisionPlayer();
        public float GetFollowSpeed();
        public float GetRushSpeed();
        public bool GetIsCanRush();
        public bool GetIsSpriteCheck();
        public MonterState GetAtackState();
        public MonsterType GetMonsterType();

        public void SetHp(int hp);
        public void SetMoveSpeed(float moveSpeed);
        public void SetAttackPower(float attackPower);
        public void SetAttackSpeed(float attackSpeed);

        public void SetJumpForce(float jumpForce);
        public void SetCanSeePlayer(bool isCanSee);
        public void SetIsCanAttack(bool isCanAttack);
        public void SetIsCollisionPlayer(bool isCollisionPlayer);
        public void SetFollowSpeed(float followSpeed);
        public void SetRushSpeed(float rushSpeed);
        public void SetIsCanRush(bool isCanRush);
        public void SetIsSpriteCheck(bool isSpriteCheck);


        public void SetAtackState(MonterState monterState);
        public void SetMonsterType(MonsterType monsterType);
        public void SetDropItemInfo();
    }
}