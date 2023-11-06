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

        protected override void Start()
        {
            base.Start();
        }

        /// <summary>
        /// ������ �� �������� �����ϸ�
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