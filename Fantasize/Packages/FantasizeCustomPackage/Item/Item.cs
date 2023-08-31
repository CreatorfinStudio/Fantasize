using Definition;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Item
{
    public class Item : MonoBehaviour
    {
        protected IPlayerInfo iplayerInfo;
        public Definition.Item ItemInfo;

        public TMP_Text infoTxt;
        private void Start()
        {
            StartCoroutine(SetIPlayerInfo());
        }

        IEnumerator SetIPlayerInfo()
        {
            while (iplayerInfo == null)
                iplayerInfo = DefinitionManager.Instance.iplayerInfo;
            yield return null;
        }
        protected virtual void Test_SetInfo(string info)
        {
            infoTxt.text = info;
        }
        protected bool IsPlayerTrigger(Collider c)
        {
            if (c.gameObject.tag.Equals("Player"))
                return true;
            else
                return false;
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
        }
    }
}