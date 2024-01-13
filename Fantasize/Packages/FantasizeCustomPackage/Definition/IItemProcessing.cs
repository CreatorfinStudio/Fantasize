using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Definition
{
    public interface IItemProcessing
    {
        /// <summary>
        /// 아이템 슬롯 저장
        /// </summary>
        //public void SaveSlotItem();

        /// <summary>
        /// 액티브 아이템 사용시 스탯 적용
        /// </summary>
        public void SetActiveItemInfo();

        public string GetSlotItemName();

        /// <summary>
        /// 습득한 인터렉션 아이템이 있는지 여부
        /// </summary>
        /// <returns></returns>
        public bool IsUseItem();

        /// <summary>
        /// 슬롯에 있는 인터렉션 아이템 사용
        /// </summary>
        public void UseItem();
    }
}
