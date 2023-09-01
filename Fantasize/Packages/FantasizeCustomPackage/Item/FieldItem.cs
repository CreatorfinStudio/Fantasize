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
        private void Update()
        {
            Test_SetInfo(itemInfo.HP + "\n" + itemInfo.hungry);
        }
        protected override void OnTriggerEnter(Collider other)
        {
            if (IsPlayerTrigger(other))
            {
                //최대 HP 능력치를 초과할 수 없다
                if (!IsExcessHP())
                {
                    this.gameObject.SetActive(false);
                    iplayerInfo.SetHp(itemInfo.HP);
                }
            }

            bool IsExcessHP()
            {
                return iplayerInfo.GetHp() + itemInfo.HP > iplayerInfo.GetMaxHP();
            }
        }

        
    }
}