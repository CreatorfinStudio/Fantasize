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

        //게임오버
        public delegate void GameOverHandler();
        public static event GameOverHandler gameOverEvent;
        //게임클리어
        public delegate void GameClearHandler();
        public static event GameClearHandler gameClearEvent;
        //게임 클리어 후 한번만 실행
        private bool isClearSet = false;

        //아이템 구매 후 처리
        public delegate void SelectItemHandler();
        public static event SelectItemHandler selectItemEvent;
        //아이템 구매 성공
        public static bool isSelectItem = false;

        //스테이지 선택 후 인게임 재진입 시 리셋팅
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
        /// 게임 일시정지
        /// </summary>
        public void PauseEditor() => EditorApplication.isPaused = true;

        /// <summary>
        /// 스테이지맵 씬 로드
        /// </summary>
        public void LoadStageMap() => SceneManager.LoadScene("StageMap");

        public void GameRestart()
        {
            gameRestartEvent?.Invoke();
        }
    }
}