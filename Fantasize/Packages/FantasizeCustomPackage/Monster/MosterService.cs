using Definition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster
{
    public class MosterService : MonoBehaviour, IMonsterInfo
    {
        public MonsterInfo monsterInfo;
  
        #region MonsterInfo Data Interface
        public float GetHp() => monsterInfo.Hp;
        public float GetMoveSpeed() => monsterInfo.WalkSpeed;
        public float GetAttackPower() => monsterInfo.AttackPower;
        public float GetAttackSpeed() => monsterInfo.AttackSpeed;
        public float GetJumpForce() => monsterInfo.JumpForce;
        public bool GetCanSeePlayer() => monsterInfo.CanSeePlayer;
        public float GetFollowSpeed() => monsterInfo.FollowSpeed;
        public float GetAtackRange() => monsterInfo.AtackRange;
        public float GetRushSpeed() => monsterInfo.RushSpeed;
        public bool GetIsCanRush() => monsterInfo.IsCanRush;
        public bool GetIsSpriteCheck() => monsterInfo.IsSpriteCheck;

        public MonterState GetAtackState() => monsterInfo.AtackState;       
        public MonsterType GetMonsterType() => monsterInfo.MonsterType;

        public void SetHp(int hp)
        {
            monsterInfo.Hp = hp;
        }

        public void SetMoveSpeed(float moveSpeed)
        {
            monsterInfo.WalkSpeed = moveSpeed;
        }

        public void SetAttackPower(float attackPower)
        {
            monsterInfo.AttackPower = attackPower;
        }
        public void SetAttackSpeed(float attackSpeed)
        {
            monsterInfo.AttackSpeed = attackSpeed;
        }

        //public void SetFollowRange(float rangedView)
        //{
        //    monsterInfo.FollowRange = rangedView;
        //}

        public void SetJumpForce(float jumpForce)
        {
            monsterInfo.JumpForce = jumpForce;
        }
        public void SetCanSeePlayer(bool isCanSee)
        {
            monsterInfo.CanSeePlayer = isCanSee;
        }
        public void SetFollowSpeed(float followSpeed)
        {
            monsterInfo.FollowSpeed = followSpeed;
        }

        public void SetAtackRange(float rangedView)
        {
            monsterInfo.AtackRange = rangedView;
        }

        public void SetRushSpeed(float rushSpeed)
        {
            monsterInfo.RushSpeed = rushSpeed;
        }
        public void SetIsCanRush(bool isCanRush)
        {
            monsterInfo.IsCanRush = isCanRush;
        }
        public void SetIsSpriteCheck(bool isSpriteCheck)
        {
            monsterInfo.IsSpriteCheck = isSpriteCheck;
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