using Definition;
using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        public static event Action gameOverEvent;
        //����Ŭ����
        public static event Action areaClearEvent;
        //�������� Ŭ���� �� �ѹ��� ����
        private bool isAreaClearSet = false;

        //�� �������� ���۽� �������� ���� �ʱ�ȭ
        public static event Action stageClearEvent;
        //�������� Ŭ���� ���� ����
        public static bool isStageClear = false;
        public bool isStageClearSet = false;

        //������ ���� �� ó��
        public static event Action selectItemEvent;
        //������ ���� ����
        public static (bool, ItemSource) isItemSelect;

        //�������� ���� �� �ΰ��� ������ �� �ʱ�ȭ
        public static event Action areaResetEvent;

        #endregion

        public IStageInfo iStageInfo;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (instance != this)
                Destroy(gameObject);

            StartCoroutine(SetIStageInfo());
        }
        private void Start()
        {
            gameOverEvent += PauseEditor;
            areaResetEvent += () => { isAreaClearSet = false; };
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

            //�������� Ŭ����
            if (isStageClear && !isStageClearSet)
            {
                isStageClearSet = true;
                isStageClear = false;
                stageClearEvent?.Invoke();
            }
            else if (!isStageClear)
                isStageClearSet = false;
        }

        public void AreaClear()
        {
            if (!isAreaClearSet && DefinitionManager.Instance.monster != null)
            {
                isAreaClearSet = true;
                areaClearEvent?.Invoke();
                DefinitionManager.Instance.iplayerInfo?.SetHaveCoin(1);
            }
        }


        //iStageInfo ����
        IEnumerator SetIStageInfo()
        {
            while (iStageInfo == null)
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
            areaResetEvent?.Invoke();

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
            areaResetEvent?.Invoke();
        }

        #endregion

    }
}