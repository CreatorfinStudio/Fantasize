using Definition;
using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    public class ShopItemService : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> shopItems = new List<GameObject>();

        [SerializeField]
        private Transform itemsGridParent;

        //최대 아이템 생성,추출 개수
        private const int itemMaxCount = 10;

        private void OnEnable()
        {
            StartCoroutine(SetShopItemInfo());
        }

        /// <summary>
        /// 샵 아이템 데이터 세팅
        /// </summary>
        /// <returns></returns>
        IEnumerator SetShopItemInfo()
        {
            while (!ReadSheetService.itemDataLoadDone)
                yield return null;

            var data = ItemService.GetRandomItems(itemMaxCount, ItemSource.ShopItem);
            for (int i = 0; i < data.Count; i++)
            {
                if (shopItems.Count < data.Count)
                    shopItems.Add(Instantiate(UIManager.Instance.itemTypePrefabs[(int)data[i].ItemGrade],
                   itemsGridParent));
                else
                    shopItems[i] = UIManager.Instance.itemTypePrefabs[(int)data[i].ItemGrade];
                shopItems[i].GetComponent<ItemService>().itemInfo = data[i];
            }
        }

        public void ResetShopItemInfo()
        {
            var data = ItemService.GetRandomItems(itemMaxCount, ItemSource.ShopItem);
            for (int i = 0; i < data.Count; i++)
            {
                shopItems[i] = UIManager.Instance.itemTypePrefabs[(int)data[i].ItemGrade];
                shopItems[i].GetComponent<ItemService>().itemInfo = data[i];
            }

            ItemService.OnProcessItemStatus(true);
        }
    }
}