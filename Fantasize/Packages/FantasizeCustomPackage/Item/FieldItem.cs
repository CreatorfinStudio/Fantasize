using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//상호작용 버튼 필요없이 다가가가면 바로 습득 및 사용
namespace Item
{
    public class FieldItem : Item
    {
        private void Update()
        {
            Test_SetInfo(ItemInfo.HP + "\n" + ItemInfo.hungry);
        }
        protected override void OnTriggerEnter(Collider other)
        {
            if (IsPlayerTrigger(other))
            {
                this.gameObject.SetActive(false);
                iplayerInfo.SetHp(ItemInfo.HP);
            }
        }

        
    }
}