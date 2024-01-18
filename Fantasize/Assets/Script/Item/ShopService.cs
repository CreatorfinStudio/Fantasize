using System.Collections;
using UnityEngine;

namespace Item
{
    public class ShopService : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] shopItems;

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

            var data = ItemService.GetRandomItems(10);
            for (int i = 0; i < data.Count; i++)
            {
                shopItems[i].SetActive(true);
                shopItems[i].GetComponent<ItemService>().itemInfo = data[i];
            }
        }

        public void ResetShopItemInfo()
        {
            var data = ItemService.GetRandomItems(10);
            for (int i = 0; i < data.Count; i++)
            {
                shopItems[i].GetComponent<ItemService>().itemInfo = data[i];
            }

            ItemService.OnProcessItemStatus(true);
        }
    }
}