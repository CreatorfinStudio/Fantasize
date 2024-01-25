using Manager;
using System.Collections;
using UnityEngine;

namespace Definition
{
    public class DefinitionManager : MonoBehaviour
    {
        #region Singleton
        private static DefinitionManager instance;
        public static DefinitionManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<DefinitionManager>();
                }
                return instance;
            }
        }
        #endregion

        public GameObject player;
        public GameObject monster;

        public IPlayerInfo iplayerInfo;
        public IMonsterInfo imonsterInfo;

        private void Awake()
        {
            GameManager.gameRestartEvent += DefinitionReset;

            //if (instance == null)
            //{
            //    instance = this;
            //    DontDestroyOnLoad(gameObject);
            //}
            //else if (instance != this)
            //    Destroy(gameObject);

            StartCoroutine(DefinitionInit());
        }

        private void DefinitionReset()
        {
            if (this != null) // �ν��Ͻ��� ��ȿ���� üũ
            {
                StartCoroutine(DefinitionInit());
            }
        }

        public static bool setDone = false;
        IEnumerator DefinitionInit()
        {
            // Player ã��
            while (player == null)
            {
                player = GameObject.Find("Player");
                yield return null;
            }

            // Monster ã��
            GameObject monsterGameObject = GameObject.Find("Monster");
            if (monsterGameObject != null && monsterGameObject.transform.childCount > 0)
            {
                monster = monsterGameObject.transform.GetChild(0).gameObject;
            }

            // Monster�� Player ���� ��������
            while (iplayerInfo == null)
            {
                if (player != null)
                    iplayerInfo = player.GetComponent<IPlayerInfo>();
                yield return null;
            }

            //���ʹ� �����Ҷ� �ٷ� �־����.
        }
    }
}