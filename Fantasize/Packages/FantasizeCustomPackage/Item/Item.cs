using Definition;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Item
{

    public class Item : MonoBehaviour , IItemProcessing
    {
        protected IPlayerInfo iplayerInfo;
        public static bool isCanUseItem = false;

        //슬롯에 등록된 아이템
        public static Definition.InteractionItem itemSlotInfo = null;        

        public TMP_Text infoTxt;
        protected virtual void Start()
        {
            StartCoroutine(SetIPlayerInfo());
        }

        IEnumerator SetIPlayerInfo()
        {
            while (iplayerInfo == null)
                iplayerInfo = DefinitionManager.Instance.iplayerInfo;
            yield return null;
        }

        /// <summary>
        /// 더미용
        /// </summary>
        /// <param name="info"></param>
        protected virtual void Test_SetInfo(string info)
        {
            infoTxt.text = info;
        }
        protected bool IsPlayerTrigger(Collider c)
        {
            if (c.gameObject.tag.Equals("Player"))
                return true;
            else
                return false;
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
        }

        #region Interface

        public void SetActiveItemInfo()
        {
            throw new System.NotImplementedException();
        }

        public string GetSlotItemName()
        {
            return itemSlotInfo?.Name;
        }

        public Definition.InteractionItem InteractionItem()
        {
            return itemSlotInfo;
        }

        public bool IsUseItem()
        {
            return isCanUseItem;
        }

        public void UseItem()
        {
            //현재 정보와 계산
            //iplayerInfo?.SetItemInfo();
            itemSlotInfo = null;
            isCanUseItem = false;
        }
        #endregion

        /// <summary>
        /// 플레이어 접근 시 F 키 발동
        /// </summary>
        /// <param name="collision"></param>
        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision == null) return;
        }
        protected bool IsPlayer(GameObject obj)
        {
            if(obj.tag.Equals("Player"))
                return true;
            else return false;
        }
        /// <summary>
        /// 플레이어가 떠날 시 F키 해제
        /// </summary>
        /// <param name="collision"></param>
        protected virtual void OnTriggerExit2D(Collider2D collision)
        {
            if(collision == null) return;
        }
    }
}