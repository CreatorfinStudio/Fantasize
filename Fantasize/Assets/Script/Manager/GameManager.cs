using Definition;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

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

        //���ӿ���
        public delegate void GameOverHandler();
        public static event GameOverHandler gameOverEvent;
        //����Ŭ����
        public delegate void GameClearHandler();
        public static event GameClearHandler gameClearEvent;
        //���� Ŭ���� �� �ѹ��� ����
        private bool isClearSet = false;

        //������ ���� �� ó��
        public delegate void SelectItemHandler();
        public static event SelectItemHandler selectItemEvent;
        //������ ���� ����
        public static (bool, ItemSource) isItemSelect;

        //�������� ���� �� �ΰ��� ������ �� ������
        public delegate void GameRestartHandler();
        public static event GameRestartHandler gameRestartEvent;

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
            if (DefinitionManager.Instance.imonsterInfo?.GetHp() <= 0 && !isClearSet)
            {
                gameClearEvent?.Invoke();
                DefinitionManager.Instance.iplayerInfo.SetHaveCoin(1);
                isClearSet = true;
            }
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
    }
}