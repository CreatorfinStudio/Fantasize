using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    public class ActiveItem : InteractionItem
    {
        public GameObject interationObj;
        
        //protected override void Start()
        //{
        //   // base.Start();
        //    StartCoroutine(CorUseItem());
        //}

        protected override void OnTriggerStay(Collider other)
        {
            if (IsPlayerTrigger(other))
            {
                ActiveInteractionPopup(interationObj, true);
                if (Input.GetKeyDown(KeyCode.F))
                {
                    GetItem(this.gameObject, itemInfo);
                }
            }
        }
        protected void GetItem(GameObject obj, Definition.InteractionItem itemInfo)
        {
            isCanUseItem = true;
            itemSlotInfo = itemInfo;
            obj?.SetActive(false);
        }

        protected override void OnTriggerExit(Collider other)
        {
            ActiveInteractionPopup(interationObj, false);
            canGetItem = false;
        }

        //protected IEnumerator CorUseItem()
        //{
        //    while (true)
        //    {
        //        Debug.Log(isCanUseItem);
        //        if (isCanUseItem && Input.GetKeyDown(KeyCode.Space))
        //        {
        //            UseItem();
        //            yield return null;
        //        }
        //        yield return new WaitForSeconds(.1f);
        //    }
        //}

        //protected void UseItem()
        //{
        //    //현재 정보와 계산
        //    iplayerInfo?.SetItemInfo();
        //    itemSlotInfo = null;
        //    isCanUseItem = false;
        //}
    }
}