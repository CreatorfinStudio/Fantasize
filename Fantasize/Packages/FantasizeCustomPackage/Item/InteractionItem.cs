using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Definition;
using UnityEditorInternal.Profiling.Memory.Experimental;

namespace Item
{
    public class InteractionItem : Item
    {
        public Definition.InteractionItem itemInfo;
        protected bool canGetItem = false;       


        protected override void Start()
        {
            base.Start();
            //StartCoroutine(CorGetItem());
         //   StartCoroutine(CorUseItem());
        }

        //private void Update()
        //{
        //    Debug.Log(isCanUseItem);
        //    if (isCanUseItem && Input.GetKeyDown(KeyCode.Space))
        //    {
        //        UseItem();
        //    }
        //}

        protected virtual void OnTriggerStay2D(Collider2D collision)
        {
            if(collision == null) return;
        }
        protected virtual void OnTriggerStay(Collider other)
        {
            if (other == null) return;

        }
        protected virtual void OnTriggerExit(Collider other)
        {
            if (other == null) return;
        }
        protected virtual void ActiveInteractionPopup(GameObject obj, bool active) => obj.SetActive(active);


        /// <summary>
        /// 상호작용 키 (F) 입력 시 아이템 획득
        /// </summary>
        /// <returns></returns>
        protected IEnumerator CorGetItem()
        {
            while (canGetItem)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {        
                    
                    canGetItem = false;

                    itemSlotInfo = itemInfo;                                     
                   //GetItem(obj);
                }
                //yield return new WaitForSeconds(.1f);
            }
            yield return null;
        }

        /// <summary>
        /// 아이템 획득 후 처리
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="itemInfo"></param>
        //protected void GetItem(GameObject obj, Definition.InteractionItem itemInfo)
        //{
        //    isCanUseItem = true;
        //    itemSlotInfo = itemInfo;
        //    obj?.SetActive(false);            
        //}

        /// <summary>
        /// 슬롯 아이템 사용
        /// </summary>
        /// <returns></returns>
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