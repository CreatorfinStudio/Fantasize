using Definition;
using Item;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("�÷��̾� HP ����")]
    [SerializeField]
    private Slider[] hpSlots;

    [Space(10)]
    [Header("���� HP �����̴�")]
    [SerializeField]
    private Slider monsterHpSlider;

    [Space(10)]
    [SerializeField]
    private GameObject gameOverUI;

    [Space(10)]
    [Header("��� ������ UI")]
    [SerializeField]
    private GameObject dropItemParent;
    [SerializeField]
    private GameObject[] dropItems;


    private void Start()
    {
        Init();
    }

    private void Update()
    {
        CurrHPToUISlot();
        CurrMonsterHPToUI();
        if (DefinitionManager.Instance.iplayerInfo.GetHp() <= 0)
        {
            gameOverUI.SetActive(true);
            PauseEditor();
        }

        SetDropItemInfo();
    }

    /// <summary>
    /// UI �ʱ�ȭ
    /// </summary>
    private void Init()
    {
        monsterHpSlider.value = 1;
    }

    public void PauseEditor()
    {
#if UNITY_EDITOR
        EditorApplication.isPaused = true;
#endif
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


    //��� ������ ���� �ʱ�ȭ������ϴ��� ����
    bool initDropItems = false;
    /// <summary>
    /// ��� ������ ���� ����
    /// </summary>
    IEnumerator SetDropItemInfo()
    {
        while (!ReadSheetService.itemDataLoadDone)
            yield return null;
        if (dropItemParent.activeSelf && !initDropItems)
        {
            var data = ItemService.GetRandomItems(3);
            for (int i = 0; i < data.Count; i++)
            {
                dropItems[i].GetComponent<ItemService>().itemInfo = data[i];
            }
            initDropItems = true;
        }
        if (!dropItemParent.activeSelf)
            initDropItems = false;
    }


}
