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

        [Header("�÷��̾� HP ����")]
        [SerializeField]
        private Slider[] hpSlots;

        [Space(5)]
        [Header("���� HP �����̴�")]
        [SerializeField]
        private Slider monsterHpSlider;

        [Space(5)]
        [SerializeField]
        private GameObject gameOverUI;


        [Space(5)]
        [Header("======== Ÿ�Ժ� ������ ������ ========")]
        [SerializeField]
        public GameObject[] itemTypePrefabs;

        [Space(5)]
        [Header("�� UI")]
        [SerializeField]
        private GameObject shopItemParent;

        [Space(5)]
        [Header("��� UI")]
        [SerializeField]
        private GameObject dropItemParent;
        [SerializeField]
        private GameObject[] dropItems;

        [Header("���� ������ �˾� �̹��� / �ӽ�")]
        [SerializeField]
        private GameObject battleStartImg;

        [Space(5)]
        [Header("Ŭ���� UI")]
        [SerializeField]
        private GameObject clearImg;

        [SerializeField]
        private GameObject testInvincibility; //���� UI. �׽�Ʈ ������ �����

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

        #region ������ UI

        /// <summary>
        /// UI �ʱ�ȭ
        /// </summary>
        private void Init()
        {
            monsterHpSlider.value = 1;
            if (dropItemParent != null)
                dropItemParent.SetActive(false);
            CheckSceneUI();
        }

        /// <summary>
        /// ���� ����Ǹ� ��Ʋ UI �˾�
        /// </summary>
        public void LoadStage()
        {
            battleStartImg.gameObject.SetActive(true);
            GameManager.Instance.GameRestart();
            GameManager.gameRestartEvent += () => { battleStartImg.gameObject.SetActive(false); };
        }

        /// <summary>
        /// ���� ���� UI
        /// </summary>
        private void GameOverUION() => gameOverUI.SetActive(true);

        /// <summary>
        /// ���� Ŭ���� UI
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
        /// ���� Scene�� ���� UI ����
        /// </summary>
        private void CheckSceneUI()
        {
            if (stageMapUI == null || inGameUI == null)
            {
                //Debug.LogError("UI ��ü�� �������� �ʾҽ��ϴ�.");
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
        /// ���� HP ���¿� ���� UI ü�¹� ǥ��
        /// </summary>
        private void CurrHPToUISlot()
        {
            if (DefinitionManager.Instance.iplayerInfo != null)
            {
                float hp = DefinitionManager.Instance.iplayerInfo.GetHp();

                for (int i = 0; i < hpSlots.Length; i++)
                {
                    // ���� �����̴��� ��Ÿ���� �ϴ� HP ���� ���
                    int sliderHpRangeStart = i * 2;
                    int sliderHpRangeEnd = sliderHpRangeStart + 1;

                    // HP�� �����̴� ������ �ʰ��ϴ� ��� �����̴��� ������ ä��.
                    if (hp > sliderHpRangeEnd)
                    {
                        if (!hpSlots[i].gameObject.activeSelf)
                            hpSlots[i].gameObject.SetActive(true);
                        hpSlots[i].value = 1f;
                    }
                    // HP�� �����̴� ���� ���� �ִ� ���
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