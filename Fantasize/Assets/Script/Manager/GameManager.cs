using Definition;
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
        public static bool isSelectItem = false;

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
        }
        private void Update()
        {
            if (DefinitionManager.Instance.iplayerInfo.GetHp() <= 0)
                gameOverEvent?.Invoke();
            if (DefinitionManager.Instance.imonsterInfo.GetHp() <= 0 && !isClearSet)
            {
                gameClearEvent?.Invoke();
                DefinitionManager.Instance.iplayerInfo.SetHaveCoin(1);
                isClearSet = true;
            }
            if (isSelectItem)
            {
                selectItemEvent?.Invoke();
                isSelectItem = false;
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
        public void LoadStageMap() => SceneManager.LoadScene("StageMap");

        public void GameRestart()
        {
            gameRestartEvent?.Invoke();
        }
    }
}