
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
        public int hp;
        public int maxHp;
        public float attackDamage;
        public float attackSpeed;
        public float moveSpeed;
        public float specialAttackDamage;
        public float castingSpeed; //특수공격 D꾹 시전시간
        public int calculation; //연산구조

        #region Property
        public string Name { get { return name; } set { name = value; } }
        public Sprite Image { get { return image; } set { image = value; } }
        public int Price { get { return price; } set { price = value; } }
        public int Hp { get { return hp; } set { hp = value; } }
        public int MaxHp { get { return maxHp; } set { maxHp = value; } }
        public float AttackDamage { get { return attackDamage; } set { attackDamage = value; } }
        public float AttackSpeed { get { return attackSpeed; } set { attackSpeed = value; } }
        public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }
        public float SpecialAttackDamage { get { return specialAttackDamage; } set { specialAttackDamage = value; } }
        public float CastingSpeed { get { return castingSpeed; } set { castingSpeed = value; } }
        public int Calculation { get { return calculation; } set { calculation = value; } }

        #endregion
    }


}