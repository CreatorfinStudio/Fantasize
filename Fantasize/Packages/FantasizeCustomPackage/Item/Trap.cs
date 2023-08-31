using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    public class Trap : Item
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