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
        public static (bool, ItemSource) isItemSelect;

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
        /// 게임 일시정지
        /// </summary>
        public void PauseEditor() => EditorApplication.isPaused = true;

        /// <summary>
        /// 스테이지맵 씬 로드
        /// </summary>
        public void LoadStageMap() => StartCoroutine(LoadStageMapScene());

        public void GameRestart() => StartCoroutine(LoadBattleScene());

        /// <summary>
        /// 전투 씬 진입
        /// </summary>
        /// <returns></returns>
        public IEnumerator LoadBattleScene()
        {
            SceneManager.LoadScene("Battle");

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Battle");

            // 씬 로딩이 완료될 때까지 대기
            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            //데이터 로딩 시간이 대체해주지않을까 싶음..일단 임시로 시간 지연
            yield return new WaitForSeconds(.5f);

            // 씬 로딩이 완료되면 후처리
            //나중에 해당 프리팹들에 맞게 로드하는 코드 추가
            gameRestartEvent?.Invoke();

        }

        /// <summary>
        /// 스테이지 선택 씬 진입
        /// </summary>
        /// <returns></returns>
        public IEnumerator LoadStageMapScene()
        {
            SceneManager.LoadScene("StageMap");
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("StageMap");

            // 씬 로딩이 완료될 때까지 대기
            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            //데이터 로딩 시간이 대체해주지않을까 싶음..일단 임시로 시간 지연
            yield return new WaitForSeconds(.5f);

            // 씬 로딩이 완료되면 후처리
            //나중에 해당 프리팹들에 맞게 로드하는 코드 추가
            gameRestartEvent?.Invoke();

        }
    }
}