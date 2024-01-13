using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Definition;
using System.Diagnostics;

namespace Item
{
    public class ItemService : MonoBehaviour, IItemProcessing
    {
        public ItemInfo itemInfo;

        #region Interface

        ////////////////////Get////////////////////
        public string GetName() => itemInfo.Name;
        public Sprite GetImage() => itemInfo.Image;
        public int GetPrice() => itemInfo.Price;
        public int GetHp() => itemInfo.Hp;
        public int GetMaxHp() => itemInfo.MaxHp;
        public float GetAttackDamage() => itemInfo.AttackDamage;
        public float GetAttackSpeed() => itemInfo.AttackSpeed;
        public float GetMoveSpeed() => itemInfo.MoveSpeed;
        public float GetSpecialAttackDamage() => itemInfo.SpecialAttackDamage;
        public float GetCastingSpeed() => itemInfo.CastingSpeed;
        public int GetCalculation() => itemInfo.Calculation;

        ////////////////////Set////////////////////
        public void SetName(string name) => itemInfo.Name = name;
        public void SetImage(Sprite img) => itemInfo.image = img;
        public void SetPrice(int price) => itemInfo.Price = price;
        public void SetHp(int hp) => itemInfo.Hp = hp;
        public void SetMaxHp(int maxhp) => itemInfo.MaxHp = maxhp;
        public void SetAttackDamage(float attackdamage) => itemInfo.AttackDamage = attackdamage;
        public void SetAttackSpeed(float attackspeed) => itemInfo.AttackSpeed = attackspeed;
        public void SetMoveSpeed(float movespeed) => itemInfo.MoveSpeed = movespeed;
        public void SetSpecialAttackDamage(float specialattackdamage) => itemInfo.SpecialAttackDamage = specialattackdamage;
        public void SetCastingSpeed(float castingspeed) => itemInfo.CastingSpeed = castingspeed;
        public void SetCalculation(int calculation) => itemInfo.Calculation = calculation;

        #endregion
    }
}