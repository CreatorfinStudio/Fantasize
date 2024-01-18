using Definition;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.Presets;
using UnityEngine;

namespace Item
{
    public class ItemService : MonoBehaviour, IItemProcessing
    {
        public ItemInfo itemInfo;
        public static List<ItemInfo> itemInfoList = new List<ItemInfo>();

        [SerializeField]
        private GameObject[] statusSlot;
        [SerializeField]
        public TMP_Text priceTxt;

        public delegate void ProcessItemStatusEventHandler<T>(T item);
        public static event ProcessItemStatusEventHandler<bool> processItemStatus;


        private void Start()
        {
            StartCoroutine(SetItemData());
            processItemStatus += ProcessItemStatus;
        }

        /// <summary>
        /// n개의 아이템 데이터 추출
        /// </summary>
        /// <param name="numberOfItems"></param>
        /// <returns></returns>
        public static List<ItemInfo> GetRandomItems(int numberOfItems)
        {
            if (itemInfoList.Count < numberOfItems)
            {
                Debug.LogError("랜덤 추출할 수보다 리스트 크기가 작음");
            }

            System.Random ran = new System.Random();

            return itemInfoList.OrderBy(x => ran.Next()).Take(numberOfItems).ToList();
        }

        IEnumerator SetItemData()
        {
            while (this.itemInfo.Name.Equals(""))
                yield return null;

            OnProcessItemStatus(false); 
        }

        public static void OnProcessItemStatus(bool isReset) => processItemStatus?.Invoke(isReset);

        public void ProcessItemStatus(bool isReset = false)
        {
            if(isReset)
            {
                for(int i = 0; i < statusSlot.Length; i++)
                {
                    statusSlot[i].SetActive(false);
                }

                isReset = false;
            }

            int countA = 0;
            if (itemInfo == null || statusSlot == null)
            {
                Debug.LogError("itemInfo 또는 statusSlot이 null입니다.");
                return;
            }

            if (itemInfo.Hp != 0 && countA < 4)
            {
                UISetting(itemInfo.Hp);
                countA++;
            }
            if (itemInfo.MaxHp != 0 && countA < 4)
            {
                UISetting(itemInfo.MaxHp);
                countA++;
            }
            if (itemInfo.AttackDamage != 0 && countA < 4)
            {
                UISetting(itemInfo.AttackDamage);
                countA++;
            }
            if (itemInfo.AttackSpeed != 0 && countA < 4)
            {
                UISetting(itemInfo.AttackSpeed);
                countA++;
            }
            if (itemInfo.MoveSpeed != 0 && countA < 4)
            {
                UISetting(itemInfo.MoveSpeed);
                countA++;
            }
            if (itemInfo.SpecialAttackDamage != 0 && countA < 4)
            {
                UISetting(itemInfo.SpecialAttackDamage);
                countA++;
            }
            if (itemInfo.CastingSpeed != 0 && countA < 4)
            {
                UISetting(itemInfo.CastingSpeed);
                countA++;
            }

            priceTxt.text = itemInfo.price.ToString();

            void UISetting(float data, string itemIconName = "none")
            {
                //일단 none

                statusSlot[countA].SetActive(true);
                var itemStatusSlot = statusSlot[countA].GetComponent<IItemStatusSlot>();
                itemStatusSlot?.SetItemImage(itemIconName);
                itemStatusSlot?.SetArrowImage(data > 0 ? "UPArrow" : "DownArrow");
            }
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