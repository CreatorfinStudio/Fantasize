using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//��ȣ�ۿ� ��ư �ʿ���� �ٰ������� �ٷ� ���� �� ���
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
                //�ִ� HP �ɷ�ġ�� �ʰ��� �� ����
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