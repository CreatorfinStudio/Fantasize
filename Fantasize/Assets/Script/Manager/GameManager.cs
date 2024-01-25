using Definition;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        #region Singleton
        private static GameManager instance;
        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<GameManager>();
                }
                return instance;
            }
        }
        #endregion

        #region Events

        //���ӿ���
        //public delegate void GameOverHandler();
        public static event Action gameOverEvent;
        //����Ŭ����
        //public delegate void GameClearHandler();
        public static event Action gameClearEvent;
        //���� Ŭ���� �� �ѹ��� ����
        private static bool isClearSet = false;

        //������ ���� �� ó��
        //public delegate void SelectItemHandler();
        public static event Action selectItemEvent;
        //������ ���� ����
        public static (bool, ItemSource) isItemSelect;

        //�������� ���� �� �ΰ��� ������ �� ������
        //public delegate void GameRestartHandler();
        public static event Action gameRestartEvent;

        #endregion

        public IStageInfo iStageInfo;

        private void Awake()
        {
            StartCoroutine(SetIStageInfo());
        }
        private void Start()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (instance != this)
                Destroy(gameObject);

            gameOverEvent += PauseEditor;
            gameRestartEvent += () => { isClearSet = false; };        
        }
        private void Update()
        {
            if (DefinitionManager.Instance.iplayerInfo?.GetHp() <= 0)
                gameOverEvent?.Invoke();
            if (isItemSelect.Item1)
            {
                selectItemEvent?.Invoke();
                GameManager.isItemSelect.Item1 = false;
                if (GameManager.isItemSelect.Item2.Equals(ItemSource.ShopItem))
                    GameRestart();
                else
                    LoadStageMap();
            }
        }

        public void StageClear()
        {
            if (!isClearSet && DefinitionManager.Instance.monster != null)
            {
                isClearSet = true;
                gameClearEvent?.Invoke();
                DefinitionManager.Instance.iplayerInfo?.SetHaveCoin(1);                
            }
        }


        //iStageInfo ����
        IEnumerator SetIStageInfo()
        {
            while(iStageInfo == null)
            {
                iStageInfo = this.GetComponent<IStageInfo>();
                yield return null;
            }
        }

        #region �� ��ȯ ������
        /// <summary>
        /// ���� �Ͻ�����
        /// </summary>
        public void PauseEditor() => EditorApplication.isPaused = true;

        /// <summary>
        /// ���������� �� �ε�
        /// </summary>
        public void LoadStageMap() => StartCoroutine(LoadStageMapScene());

        public void GameRestart() => StartCoroutine(LoadBattleScene());

        /// <summary>
        /// ���� �� ����
        /// </summary>
        /// <returns></returns>
        public IEnumerator LoadBattleScene()
        {
            SceneManager.LoadScene("Battle");

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Battle");

            // �� �ε��� �Ϸ�� ������ ���
            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            //������ �ε� �ð��� ��ü������������ ����..�ϴ� �ӽ÷� �ð� ����
            yield return new WaitForSeconds(.5f);

            // �� �ε��� �Ϸ�Ǹ� ��ó��
            //���߿� �ش� �����յ鿡 �°� �ε��ϴ� �ڵ� �߰�
            gameRestartEvent?.Invoke();

        }

        /// <summary>
        /// �������� ���� �� ����
        /// </summary>
        /// <returns></returns>
        public IEnumerator LoadStageMapScene()
        {
            SceneManager.LoadScene("StageMap");
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("StageMap");

            // �� �ε��� �Ϸ�� ������ ���
            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            //������ �ε� �ð��� ��ü������������ ����..�ϴ� �ӽ÷� �ð� ����
            yield return new WaitForSeconds(.5f);

            // �� �ε��� �Ϸ�Ǹ� ��ó��
            //���߿� �ش� �����յ鿡 �°� �ε��ϴ� �ڵ� �߰�
            gameRestartEvent?.Invoke();
        }

        #endregion

    }
}