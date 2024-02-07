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

        //�ִ� ������ ����,���� ����
        private const int itemMaxCount = 10;

        private void OnEnable()
        {
            StartCoroutine(SetShopItemInfo());
        }

        /// <summary>
        /// �� ������ ������ ����
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
        /// �����Ǿ��ִ� ������ ����. 
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