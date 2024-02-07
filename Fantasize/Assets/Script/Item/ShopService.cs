using Definition;
using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    public class ShopService : MonoBehaviour
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

            DestroyItemPrefabs();

            var data = ItemService.GetRandomItems(itemMaxCount, ItemSource.ShopItem);
            for (int i = 0; i < data.Count; i++)
            {
                shopItems.Add(Instantiate(UIManager.Instance.itemTypePrefabs[(int)data[i].ItemGrade],
                    itemsGridParent));
                shopItems[i].GetComponent<ItemService>().itemInfo = data[i];
            }
        }

        public void ResetShopItemInfo()
        {
            DestroyItemPrefabs();

            var data = ItemService.GetRandomItems(itemMaxCount, ItemSource.ShopItem);
            for (int i = 0; i < data.Count; i++)
            {
                shopItems.Add(Instantiate(UIManager.Instance.itemTypePrefabs[(int)data[i].ItemGrade],
                    itemsGridParent));
                shopItems[i].GetComponent<ItemService>().itemInfo = data[i];
            }

            ItemService.OnProcessItemStatus(true);
        }

        /// <summary>
        /// 생성되어있는 프리팹 삭제. 
        /// </summary>
        private void DestroyItemPrefabs()
        {
            if (itemsGridParent.childCount == 0)
                return;
            for (int i = 0; i < shopItems.Count; i++)
                Destroy(shopItems[i]);
            shopItems.Clear();
        }    
    }
}