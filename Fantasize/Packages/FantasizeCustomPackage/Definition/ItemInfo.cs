using System;
using UnityEngine;
using UnityEngine.UI;

namespace Definition
{
    [System.Serializable]
    public class ItemInfo
    {
        public string name;
        public Sprite image;
        [Space(10)]
        public int price;
        [Space(10)]
        public float hp;
        public float maxHp;
        public float attackDamage;
        public float attackSpeed;
        public float moveSpeed;
        public float specialAttackDamage;
        public float castingSpeed; //특수공격 D꾹 시전시간
        public ItemCalculation calculation; //연산구조

        public ItemSource itemSource;

        #region Property
        public string Name { get { return name; } set { name = value; } }
        public Sprite Image { get { return image; } set { image = value; } }
        public int Price { get { return price; } set { price = value; } }
        public float Hp { get { return hp; } set { hp = value; } }
        public float MaxHp { get { return maxHp; } set { maxHp = value; } }
        public float AttackDamage { get { return attackDamage; } set { attackDamage = value; } }
        public float AttackSpeed { get { return attackSpeed; } set { attackSpeed = value; } }
        public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }
        public float SpecialAttackDamage { get { return specialAttackDamage; } set { specialAttackDamage = value; } }
        public float CastingSpeed { get { return castingSpeed; } set { castingSpeed = value; } }
        public ItemCalculation Calculation { get { return calculation; } set { calculation = value; } }
        public ItemSource ItemSource { get { return itemSource; } set { itemSource = value; } }

        #endregion
    }

    [System.Serializable]
    public class StatusSlotInfo
    {   
        public Image itemIcon;
        public Image arrowIcon;

        public Image ItemIcon { get { return itemIcon; } set { itemIcon = value; } }
        public Image ArrowIcon { get { return arrowIcon; } set { arrowIcon = value; } }
    }
}