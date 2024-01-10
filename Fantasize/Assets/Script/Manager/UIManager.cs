using Definition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Slider[] sliders;
    [SerializeField]
    private GameObject gameOverUI;

    private void Update()
    {
        CurrHPToUISlot();
        if(DefinitionManager.Instance.iplayerInfo.GetHp() <= 0)
        {
            gameOverUI.SetActive(true);
            PauseEditor();
        }
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
        int hp = DefinitionManager.Instance.iplayerInfo.GetHp();

        for (int i = 0; i < sliders.Length; i++)
        {
            // 현재 슬라이더가 나타내야 하는 HP 범위 계산
            int sliderHpRangeStart = i * 2;
            int sliderHpRangeEnd = sliderHpRangeStart + 1;
            
            // HP가 슬라이더 범위를 초과하는 경우 슬라이더를 완전히 채움.
            if (hp > sliderHpRangeEnd)
            {
                if (!sliders[i].gameObject.activeSelf)
                    sliders[i].gameObject.SetActive(true);
                sliders[i].value = 1f;
            }
            // HP가 슬라이더 범위 내에 있는 경우
            else if (hp > sliderHpRangeStart)
            {
                if (!sliders[i].gameObject.activeSelf)
                    sliders[i].gameObject.SetActive(true);
                sliders[i].value = (hp % 2 != 0) ? 0.5f : 1f; 
            }
            else
            {
                sliders[i].value = 0f;
            }
        }
    }
}
