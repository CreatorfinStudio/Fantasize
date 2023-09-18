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
        /// 유저가 이 아이템을 습득하면
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