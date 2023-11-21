using System.Collections;
using System.Collections.Generic;
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
        public GameObject itemDummy;
        public GameObject monsters;

        public IPlayerInfo iplayerInfo;
        public IMonsterInfo imonsterInfo;
        public IItemProcessing iItemProcessing; 

        private void Awake()
        {
            SetInit();
        }

        private void SetInit()
        {
            iplayerInfo = player.GetComponent<IPlayerInfo>();
            imonsterInfo = monsters.GetComponent<IMonsterInfo>();
            iItemProcessing = itemDummy.GetComponent<IItemProcessing>();            
        }
    }
}