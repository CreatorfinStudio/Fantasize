using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Definition;

namespace Item
{
    public class Trap : Item
    {
        public Definition.Trap trapInfo;
        private void Update()
        {
            Test_SetInfo(trapInfo.HP + "\n" + trapInfo.hungry);

        }
        protected override void OnTriggerEnter(Collider other)
        {
            if (IsPlayerTrigger(other))
            {
                this.gameObject.SetActive(false);
                iplayerInfo.SetHp(trapInfo.HP);
            }
        }
    }
}