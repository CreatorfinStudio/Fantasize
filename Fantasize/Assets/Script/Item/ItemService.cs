using Definition;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Item
{
    public class ItemService : MonoBehaviour, IItemProcessing
    {
        public ItemInfo itemInfo;
        public static List<ItemInfo> itemInfoList = new List<ItemInfo>();

        public static List<ItemInfo> GetRandomItems(int numberOfItems)
        {
            if (itemInfoList.Count < numberOfItems)
            {
                Debug.LogError("랜덤 추출할 수보다 리스트 크기가 작음");
            }

            System.Random ran = new System.Random();

            return itemInfoList.OrderBy(x => ran.Next()).Take(numberOfItems).ToList();
        }

        #region Interface

        ////////////////////Get////////////////////
        public string GetName() => itemInfo.Name;
        public Sprite GetImage() => itemInfo.Image;
        public int GetPrice() => itemInfo.Price;
        public float GetHp() => itemInfo.Hp;
        public float GetMaxHp() => itemInfo.MaxHp;
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
        public void SetHp(float hp) => itemInfo.Hp = hp;
        public void SetMaxHp(float maxhp) => itemInfo.MaxHp = maxhp;
        public void SetAttackDamage(float attackdamage) => itemInfo.AttackDamage = attackdamage;
        public void SetAttackSpeed(float attackspeed) => itemInfo.AttackSpeed = attackspeed;
        public void SetMoveSpeed(float movespeed) => itemInfo.MoveSpeed = movespeed;
        public void SetSpecialAttackDamage(float specialattackdamage) => itemInfo.SpecialAttackDamage = specialattackdamage;
        public void SetCastingSpeed(float castingspeed) => itemInfo.CastingSpeed = castingspeed;
        public void SetCalculation(int calculation) => itemInfo.Calculation = calculation;

        #endregion
    }
}