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

        //���Կ� ��ϵ� ������
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
        /// ���̿�
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
            //���� ������ ���
            //iplayerInfo?.SetItemInfo();
            itemSlotInfo = null;
            isCanUseItem = false;
        }


        #endregion
    }
}