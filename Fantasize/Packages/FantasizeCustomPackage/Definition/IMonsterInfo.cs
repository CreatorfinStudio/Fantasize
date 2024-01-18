using UnityEngine;

namespace Definition
{
    public interface IMonsterInfo
    {
        public float GetHp();
        public float GetMaxHp();
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
        public bool GetIsDirectionCheck();
        public BoxCollider2D GetAttackCollider();

        public MonterState GetAtackState();
        public MonsterType GetMonsterType();

        public void SetHp(float hp);
        public void SetMaxHp(float maxhp);
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
        public void SetIsDirectionCheck(bool isDirectionCheck);
        public void SetAttackCollider(BoxCollider2D attackCollider);


        public void SetAtackState(MonterState monterState);
        public void SetMonsterType(MonsterType monsterType);
        public void SetDropItemInfo();
    }
}