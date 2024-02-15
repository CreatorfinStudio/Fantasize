using Definition;
using Manager;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Item
{
    //���� ������ ���Կ� ���� Ŭ����
    public class ItemService : MonoBehaviour, IItemProcessing
    {
        public ItemInfo itemInfo;
        public static List<ItemInfo> itemInfoList = new List<ItemInfo>();

        [SerializeField]
        private GameObject[] statusSlot;
        [SerializeField]
        public TMP_Text priceTxt;

        public static System.Action<bool> processItemStatus;

        private void Start()
        {
            StartCoroutine(SetItemData());
            processItemStatus += ProcessItemStatus;
        }

        /// <summary>
        /// Ÿ�Կ� �´� n���� ������ ������ ����
        /// </summary>
        /// <param name="numberOfItems"></param>
        /// <returns></returns>
        public static List<ItemInfo> GetRandomItems(int numberOfItems, ItemSource type)
        {
            List<ItemInfo> filteredList = new List<ItemInfo>();

            // ������ �ҽ��� ���� ���͸�
            if (type == ItemSource.ShopItem || type == ItemSource.CommonItem)
                filteredList = itemInfoList.Where(item => item.ItemSource == ItemSource.ShopItem || item.ItemSource == ItemSource.CommonItem).ToList();
            else if (type == ItemSource.DropItem || type == ItemSource.CommonItem)
                filteredList = itemInfoList.Where(item => item.ItemSource == ItemSource.DropItem || item.ItemSource == ItemSource.CommonItem).ToList();

            // ���͸��� ����Ʈ�� ũ�� Ȯ��
            if (filteredList.Count < numberOfItems)
            {
                Debug.LogError("���� ������ ������ ����Ʈ ũ�Ⱑ ����");
                return new List<ItemInfo>(); // �Ǵ� ������ ���� ó��
            }

            List<ItemInfo> selectedItems = new List<ItemInfo>();

            // Ȯ���� ���� ������ ����
            while (selectedItems.Count < numberOfItems)
            {
                // �� ������ ��޺� Ȯ���� ���� �������� ����
                double randomNumber = new System.Random().NextDouble();
                double cumulativeProbability = 0.0;

                foreach (var item in filteredList)
                {
                    double itemProbability = GetItemGradeProbability(item.ItemGrade);
                    cumulativeProbability += itemProbability;

                    if (randomNumber <= cumulativeProbability)
                    {
                        selectedItems.Add(item);
                        filteredList.Remove(item);
                        break; 
                    }
                }
            }

            return selectedItems;
        }

        // ������ ��޺� Ȯ�� ��ȯ
        public static double GetItemGradeProbability(ItemGrade grade)
        {
            switch (grade)
            {
                case ItemGrade.Common: return 0.4;
                case ItemGrade.Rare: return 0.3;
                case ItemGrade.Unique: return 0.2;
                case ItemGrade.Legend: return 0.1;
                default: return 0.0;
            }
        }

        IEnumerator SetItemData()
        {
            while (this.itemInfo.Name.Equals(""))
                yield return null;

            ProcessItemStatus(false);
        }

        public static void OnProcessItemStatus(bool isReset) => processItemStatus?.Invoke(isReset);

        /// <summary>
        /// �� �������� ��� �ε� �����͸� �� ������ �� ���Կ� ����
        /// </summary>
        /// <param name="isReset"></param>
        public void ProcessItemStatus(bool isReset = false)
        {
            //��Ӿ������� ��쿡�� �� ���� ǥ�� X
            if (statusSlot.Length == 0)
                return;

            if (isReset)
            {
                for (int i = 0; i < statusSlot.Length; i++)
                {
                    if (statusSlot[i] != null)
                        statusSlot[i].SetActive(false);
                }

                isReset = false;
            }

            int countA = 0;
            if (itemInfo == null || this.itemInfo.Name.Equals(""))
            {
                Debug.LogError("itemInfo �� null��");
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

            if (priceTxt != null)
                priceTxt.text = itemInfo.price.ToString();

            void UISetting(float data, string itemIconName = "none")
            {
                //�ϴ� itemIconName none
                if (statusSlot[countA] != null)
                {
                    statusSlot[countA].SetActive(true);
                    var itemStatusSlot = statusSlot[countA].GetComponent<IItemStatusSlot>();
                    itemStatusSlot?.SetItemImage(itemIconName);
                    itemStatusSlot?.SetArrowImage(data > 0 ? "UPArrow" : "DownArrow");
                }
            }
        }

        public void OnClickSelectItem()
        {
            //�տ����϶�
            if (itemInfo.Calculation == ItemCalculation.Addition || itemInfo.Calculation == ItemCalculation.None)
            {
                if (UIManager.Instance.stageMapUI.activeSelf)
                {
                    DefinitionManager.Instance.iplayerInfo.SetAddItemStatsToPlayer(itemInfo, true);
                    UIManager.Instance.shopItemParent.SetActive(false);
                }
                else
                    DefinitionManager.Instance.iplayerInfo.SetAddItemStatsToPlayer(itemInfo, false);
            }
            else
            {
                if (UIManager.Instance.stageMapUI.activeSelf)
                {
                    DefinitionManager.Instance.iplayerInfo.SetMultiplicationItemStatsToPlayer(itemInfo, true);
                    UIManager.Instance.shopItemParent.SetActive(false);
                }
                else
                    DefinitionManager.Instance.iplayerInfo.SetMultiplicationItemStatsToPlayer(itemInfo, false);
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
        public ItemCalculation GetCalculation() => itemInfo.Calculation;

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
        public void SetCalculation(ItemCalculation calculation) => itemInfo.Calculation = calculation;

        #endregion
    }
}