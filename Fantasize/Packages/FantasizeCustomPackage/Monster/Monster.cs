using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Definition;

namespace Monster
{
    public class Monster : MonoBehaviour
    {
        // 몬스터 List는 다른곳에서 관리해야 좋을 듯

        public Transform playerPosi;

        protected IMonsterInfo imonsterInfo;

        protected virtual void Start()
        {
            StartCoroutine(SetIPlayerInfo());
        }

        private void Update()
        {
           playerPosi = DefinitionManager.Instance.player.transform;
        }

        protected virtual IEnumerator SetIPlayerInfo()
        {
            while (imonsterInfo == null)
            {
                imonsterInfo = DefinitionManager.Instance.imonsterInfo;
                yield return null;
            }
        }

    }
}
