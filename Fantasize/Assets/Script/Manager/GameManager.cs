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

        //게임오버
        public static event Action gameOverEvent;
        //게임클리어
        public static event Action areaClearEvent;
        //전투구역 클리어 후 한번만 실행
        private bool isAreaClearSet = false;

        //새 스테이지 시작시 스테이지 정보 초기화
        public static event Action stageClearEvent;
        //스테이지 클리어 여부 전달
        public static bool isStageClear = false;
        public bool isStageClearSet = false;

        //아이템 구매 후 처리
        public static event Action selectItemEvent;
        //아이템 구매 성공
        public static (bool, ItemSource) isItemSelect;

        //전투구역 선택 후 인게임 재진입 시 초기화
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

            //스테이지 클리어
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


        //iStageInfo 세팅
        IEnumerator SetIStageInfo()
        {
            while (iStageInfo == null)
            {
                iStageInfo = this.GetComponent<IStageInfo>();
                yield return null;
            }
        }

        #region 씬 전환 시퀀스
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
            areaResetEvent?.Invoke();

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
            areaResetEvent?.Invoke();
        }

        #endregion

    }
}