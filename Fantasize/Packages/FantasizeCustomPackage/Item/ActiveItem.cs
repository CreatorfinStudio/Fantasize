using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Definition;

namespace Item
{
    public class ActiveItem : InteractionItem
    {
        Animator animator;
        protected override void Start()
        {
            base.Start();
            animator = GetComponent<Animator>();

        }

        /// <summary>
        /// ������ �� �������� �����ϸ�
        /// </summary>
        /// <param name="collision"></param>
        protected override void OnTriggerStay2D(Collider2D collision)
        {
            if (IsPlayer(collision.gameObject))
            {
                if (Input.GetKey(KeyCode.F))
                {
                    iplayerInfo?.SetItemInfo(itemInfo);
                    this.gameObject.SetActive(false);
                }
            }
        }

    }
}