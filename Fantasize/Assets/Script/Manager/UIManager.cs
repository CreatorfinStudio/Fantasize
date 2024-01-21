using Definition;
using Item;
using UnityEngine;
using UnityEngine.UI;

namespace Manager
{
    public class UIManager : MonoBehaviour
    {
        private static UIManager instance;

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
        [Header("��� ������ UI")]
        [SerializeField]
        private GameObject dropItemParent;
        [SerializeField]
        private GameObject[] dropItems;

        [SerializeField]
        private GameObject testInvincibility; //���� UI. �׽�Ʈ ������ �����

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
        /// UI �ʱ�ȭ
        /// </summary>
        private void Init()
        {
            monsterHpSlider.value = 1;
        }


        /// <summary>
        /// ���� HP ���¿� ���� UI ü�¹� ǥ��
        /// </summary>
        private void CurrHPToUISlot()
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
            //����������� ��� ����â OFF
            if (button.gameObject.name.Contains("DropItem"))
                GameManager.selectItemEvent += OFFDropItemUI;
        }

        private void OFFDropItemUI() => dropItemParent.SetActive(false);

    }
}