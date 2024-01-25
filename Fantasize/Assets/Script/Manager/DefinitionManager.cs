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
            if (this != null) // 인스턴스가 유효한지 체크
            {
                StartCoroutine(DefinitionInit());
            }
        }

        public static bool setDone = false;
        IEnumerator DefinitionInit()
        {
            // Player 찾기
            while (player == null)
            {
                player = GameObject.Find("Player");
                yield return null;
            }

            // Monster 찾기
            GameObject monsterGameObject = GameObject.Find("Monster");
            if (monsterGameObject != null && monsterGameObject.transform.childCount > 0)
            {
                monster = monsterGameObject.transform.GetChild(0).gameObject;
            }

            // Monster와 Player 정보 가져오기
            while (iplayerInfo == null)
            {
                if (player != null)
                    iplayerInfo = player.GetComponent<IPlayerInfo>();
                yield return null;
            }

            //몬스터는 생성할때 바로 넣어버림.
        }
    }
}