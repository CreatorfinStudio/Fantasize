using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Definition;

namespace Monster
{
    public class Monster : MonoBehaviour
    {
        // ���� List�� �ٸ������� �����ؾ� ���� ��

        public Transform playerPosi;
        public IMonsterInfo imonsterInfo;

        protected void Start()
        {
            StartCoroutine(SetIMonsterInfo());
        }

        private void Update()
        {
           playerPosi = DefinitionManager.Instance.player.transform;
        }

        protected IEnumerator SetIMonsterInfo()
        {
            while (imonsterInfo == null)
            {
                imonsterInfo = DefinitionManager.Instance.imonsterInfo;
                yield return null;
            }
        }
        protected virtual void SetSpriteFlipX(Transform trans)
        {

        }
    }
}
