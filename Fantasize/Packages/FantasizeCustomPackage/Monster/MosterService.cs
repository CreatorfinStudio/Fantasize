using Definition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster
{
    public class MosterService : MonoBehaviour, IMonsterInfo
    {
        public MonsterInfo monsterInfo;
        private void Start()
        {
            SetComponent();
        }

        private void SetComponent()
        {
            this.gameObject.AddComponent<SmallMonster>();
        }

        #region MonsterInfo Data Interface
        public float GetHp() => monsterInfo.Hp;
        public float GetMoveSpeed() => monsterInfo.MoveSpeed;
        public float GetAttackPower() => monsterInfo.AttackPower;
        public float GetAttackSpeed() => monsterInfo.AttackSpeed;
        public float GetFollowRange() => monsterInfo.FollowRange;
        public float GetAtackRange() => monsterInfo.AtackRange;
        public MonterState GetAtackState() => monsterInfo.AtackState;       
        public MonsterType GetMonsterType() => monsterInfo.MonsterType;

        public void SetHp(int hp)
        {
            monsterInfo.Hp = hp;
        }

        public void SetMoveSpeed(float moveSpeed)
        {
            monsterInfo.MoveSpeed = moveSpeed;
        }

        public void SetAttackPower(float attackPower)
        {
            monsterInfo.AttackPower = attackPower;
        }

        public void SetAttackSpeed(float attackSpeed)
        {
            monsterInfo.AttackSpeed = attackSpeed;
        }

        public void SetFollowRange(float rangedView)
        {
            monsterInfo.FollowRange = rangedView;
        }

        public void SetAtackRange(float rangedView)
        {
            monsterInfo.AtackRange = rangedView;
        }

        public void SetAtackState(MonterState monterState)
        {
            monsterInfo.AtackState = monterState;
        }

        public void SetMonsterType(MonsterType monsterType)
        {
            monsterInfo.MonsterType = monsterType;
        }

        /// <summary>
        /// 드랍 아이템 정보
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void SetDropItemInfo()
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}