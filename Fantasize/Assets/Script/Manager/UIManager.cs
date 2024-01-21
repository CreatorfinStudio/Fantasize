using Definition;
using Item;
using UnityEngine;
using UnityEngine.UI;

namespace Manager
{
    public class UIManager : MonoBehaviour
    {
        private static UIManager instance;

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
        [Header("드롭 아이템 UI")]
        [SerializeField]
        private GameObject dropItemParent;
        [SerializeField]
        private GameObject[] dropItems;

        [SerializeField]
        private GameObject testInvincibility; //무적 UI. 테스트 끝나면 지우기

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
            GameManager.gameClearEvent += DropItemON;
        }

        private void Update()
        {
            CurrHPToUISlot();
            CurrMonsterHPToUI();
        }

        private void GameOverUION() => gameOverUI.SetActive(true);
        private void DropItemON()
        {
            dropItemParent.SetActive(true);
            testInvincibility.SetActive(false);

            var data = ItemService.GetRandomItems(3, ItemSource.DropItem);

            for (int i = 0; i < data.Count; i++)
            {
                dropItems[i].SetActive(true);
                dropItems[i].GetComponent<ItemService>().itemInfo = data[i];
            }

        }

        /// <summary>
        /// UI 초기화
        /// </summary>
        private void Init()
        {
            monsterHpSlider.value = 1;
        }


        /// <summary>
        /// 현재 HP 상태에 따른 UI 체력바 표기
        /// </summary>
        private void CurrHPToUISlot()
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
                }
            }
        }

        private void CurrMonsterHPToUI()
        {
            monsterHpSlider.value = DefinitionManager.Instance.imonsterInfo.GetHp() /
                 DefinitionManager.Instance.imonsterInfo.GetMaxHp();
        }

        public void OnClickSelectItem(Button button)
        {
            //드랍아이템의 경우 선택창 OFF
            if (button.gameObject.name.Contains("DropItem"))
                GameManager.selectItemEvent += OFFDropItemUI;
        }

        private void OFFDropItemUI() => dropItemParent.SetActive(false);

    }
}