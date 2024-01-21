using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Manager;

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
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (instance != this)
                Destroy(gameObject);

            SetInit();
        }

        private void SetInit()
        {
            iplayerInfo = player.GetComponent<IPlayerInfo>();
            imonsterInfo = monster.GetComponent<IMonsterInfo>();

            GameManager.gameRestartEvent += DefinitionReset;
        }

        private void DefinitionReset() => StartCoroutine(DefinitionInit());
        IEnumerator DefinitionInit()
        {
            while (player == null)
            {
                player = GameObject.Find("Player");
                yield return null;
            }
            while (monster == null)
            {
                monster = GameObject.Find("Monster")?.transform.GetChild(0).gameObject;
                yield return null;
            }
        }
    }
}