using Definition;
using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    public class DropItemService : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> dropItems = new List<GameObject>();


        [SerializeField]
        private Transform itemsGridParent;

        //최대 아이템 생성,추출 개수
        private const int itemMaxCount = 3;

        private void OnEnable()
        {
            StartCoroutine(SetDropItemInfo());
        }

        /// <summary>
        /// 드롭 아이템 데이터 세팅
        /// </summary>
        /// <returns></returns>
        IEnumerator SetDropItemInfo()
        {
            while (!ReadSheetService.itemDataLoadDone)
                yield return null;

            var data = ItemService.GetRandomItems(itemMaxCount, ItemSource.DropItem);
            for (int i = 0; i < data.Count; i++)
            {
                if (dropItems.Count < data.Count)
                    dropItems.Add(Instantiate(UIManager.Instance.itemTypePrefabs[(int)data[i].ItemGrade],
                   itemsGridParent));
                else
                    dropItems[i] = UIManager.Instance.itemTypePrefabs[(int)data[i].ItemGrade];
                dropItems[i].GetComponent<ItemService>().itemInfo = data[i];
                dropItems[i].gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
}