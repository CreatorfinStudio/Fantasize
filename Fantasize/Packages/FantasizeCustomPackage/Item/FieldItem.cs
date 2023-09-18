using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//상호작용 버튼 필요없이 다가가가면 바로 습득 및 사용
namespace Item
{
    public class FieldItem : Item
    {
        public Definition.FieldItem itemInfo;

        protected override void Start()
        {
            base.Start();
        }

        /// <summary>
        /// 유저가 이 아이템을 습득하면
        /// </summary>
        /// <param name="collision"></param>
        protected void OnTriggerStay2D(Collider2D collision)
        {
            if (IsPlayer(collision.gameObject))
            {
                iplayerInfo?.SetItemInfo(itemInfo);
                this.gameObject.SetActive(false);
            }
        }

    }
}