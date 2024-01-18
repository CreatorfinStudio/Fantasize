using Definition;
using Item;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("플레이어 HP 슬롯")]
    [SerializeField]
    private Slider[] hpSlots;

    [Space(10)]
    [Header("몬스터 HP 슬라이더")]
    [SerializeField]
    private Slider monsterHpSlider;

    [Space(10)]
    [SerializeField]
    private GameObject gameOverUI;

    [Space(10)]
    [Header("드롭 아이템 UI")]
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
    /// UI 초기화
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


    //드랍 아이템 정보 초기화해줘야하는지 여부
    bool initDropItems = false;
    /// <summary>
    /// 드랍 아이템 정보 세팅
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
