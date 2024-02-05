using Definition;
using Item;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Manager
{
    public class UIManager : MonoBehaviour
    {
        #region Singleton
        private static UIManager instance;
        public static UIManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<UIManager>();
                }
                return instance;
            }
        }
        #endregion

        #region variable

        [SerializeField]
        private GameObject stageMapUI;
        [SerializeField]
        private GameObject inGameUI;

        [Header("플레이어 HP 슬롯")]
        [SerializeField]
        private Slider[] hpSlots;

        [Space(5)]
        [Header("몬스터 HP 슬라이더")]
        [SerializeField]
        private Slider monsterHpSlider;

        [Space(5)]
        [SerializeField]
        private GameObject gameOverUI;


        [Space(5)]
        [Header("======== 타입별 아이템 프리팹 ========")]
        [SerializeField]
        public GameObject[] itemTypePrefabs;

        [Space(5)]
        [Header("샵 UI")]
        [SerializeField]
        private GameObject shopItemParent;

        [Space(5)]
        [Header("드롭 UI")]
        [SerializeField]
        private GameObject dropItemParent;
        [SerializeField]
        private GameObject[] dropItems;

        [Header("전투 시작전 팝업 이미지 / 임시")]
        [SerializeField]
        private GameObject battleStartImg;

        [Space(5)]
        [Header("클리어 UI")]
        [SerializeField]
        private GameObject clearImg;

        [SerializeField]
        private GameObject testInvincibility; //무적 UI. 테스트 끝나면 지우기

        #endregion

        private void Start()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (instance != this)
                Destroy(gameObject);

            Init();

            GameManager.gameOverEvent += GameOverUION;
            GameManager.gameClearEvent += ONClearUINDropItemUI;
            GameManager.gameRestartEvent += Init;
            GameManager.gameRestartEvent += CheckSceneUI;
        }

        private void Update()
        {
            CurrHPToUISlot();
            CurrMonsterHPToUI();
        }

        #region 시퀀스 UI

        /// <summary>
        /// UI 초기화
        /// </summary>
        private void Init()
        {
            monsterHpSlider.value = 1;
            if (dropItemParent != null)
                dropItemParent.SetActive(false);
            CheckSceneUI();
        }

        /// <summary>
        /// 샵이 종료되면 배틀 UI 팝업
        /// </summary>
        public void LoadStage()
        {
            battleStartImg.gameObject.SetActive(true);
            GameManager.Instance.GameRestart();
            GameManager.gameRestartEvent += () => { battleStartImg.gameObject.SetActive(false); };
        }

        /// <summary>
        /// 게임 오버 UI
        /// </summary>
        private void GameOverUION() => gameOverUI.SetActive(true);

        /// <summary>
        /// 게임 클리어 UI
        /// </summary>
        private void ONClearUINDropItemUI()
        {
            if(this != null)
                StartCoroutine(CorONClearUINDropItemUI());
        }

        IEnumerator CorONClearUINDropItemUI()
        {
            if (clearImg == null || dropItemParent == null)
                yield return null;

            clearImg.SetActive(true);
            yield return new WaitForSeconds(.5f);
            clearImg.SetActive(false);       

            dropItemParent?.SetActive(true);
            testInvincibility?.SetActive(false);

            var data = ItemService.GetRandomItems(3, ItemSource.DropItem);

            for (int i = 0; i < data.Count; i++)
            {
                dropItems[i].SetActive(true);
                dropItems[i].GetComponent<ItemService>().itemInfo = data[i];
            }
        }

        /// <summary>
        /// 현재 Scene에 따른 UI 설정
        /// </summary>
        private void CheckSceneUI()
        {
            if (stageMapUI == null || inGameUI == null)
            {
                //Debug.LogError("UI 객체가 참조되지 않았습니다.");
                return;
            }

            switch (SceneManager.GetActiveScene().name)
            {
                case "StageMap":
                    stageMapUI.SetActive(true);
                    inGameUI.SetActive(false);
                    break;
                case "Battle":
                    stageMapUI.SetActive(false);
                    inGameUI.SetActive(true);
                    break;
            }
        }

        #endregion

        #region HP Bar
        /// <summary>
        /// 현재 HP 상태에 따른 UI 체력바 표기
        /// </summary>
        private void CurrHPToUISlot()
        {
            if (DefinitionManager.Instance.iplayerInfo != null)
            {
                float hp = DefinitionManager.Instance.iplayerInfo.GetHp();

                for (int i = 0; i < hpSlots.Length; i++)
                {
                    // 현재 슬라이더가 나타내야 하는 HP 범위 계산
                    int sliderHpRangeStart = i * 2;
                    int sliderHpRangeEnd = sliderHpRangeStart + 1;

                    // HP가 슬라이더 범위를 초과하는 경우 슬라이더를 완전히 채움.
                    if (hp > sliderHpRangeEnd)
                    {
                        if (!hpSlots[i].gameObject.activeSelf)
                            hpSlots[i].gameObject.SetActive(true);
                        hpSlots[i].value = 1f;
                    }
                    // HP가 슬라이더 범위 내에 있는 경우
                    else if (hp > sliderHpRangeStart)
                    {
                        if (!hpSlots[i].gameObject.activeSelf)
                            hpSlots[i].gameObject.SetActive(true);
                        hpSlots[i].value = (hp % 2 != 0) ? 0.5f : 1f;
                    }
                    else
                    {
                        hpSlots[i].value = 0f;
                        hpSlots[i].gameObject.SetActive(false);
                    }
                }
            }
        }

        private void CurrMonsterHPToUI()
        {
            if (DefinitionManager.Instance.imonsterInfo != null)
                monsterHpSlider.value = DefinitionManager.Instance.imonsterInfo.GetHp() /
                     DefinitionManager.Instance.imonsterInfo.GetMaxHp();
        }
        #endregion

        public void OnClickSelectItem(Button button)
        {
            if (button.gameObject.name.Contains("ShopItem"))
                GameManager.selectItemEvent += () => shopItemParent.SetActive(false);
        }

    }
}