using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Definition
{
    public interface IPlayerInfo
    {
        public int GetHp();
        public int GetHungry();

        public float GetMoveSpeed();
        public float GetAttackPower();
        public float GetAttackSpeed();
        public float GetRotationSpeed();

        public float GetRangedView();
        public float GetForwardView();

        public int GetMaxHP();
        public int GetMaxHungry();

        public void SetHp(int hp);
        public void SetHungry(int hungry);

        public void SetMoveSpeed(float moveSpeed);
        public void SetAttackPower(float attackPower);
        public void SetAttackSpeed(float attackSpeed);
        public void SetRotationSpeed(float rotationSpeed);
        public void SetRangedView(float rangedView);
        public void SetForwardView(float forwardView);
        public void SetMaxHungry(int maxHungry);
        public void SetMaxHP(int maxHP);

    }
}