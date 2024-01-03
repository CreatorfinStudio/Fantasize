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
    /// ���� HP ���¿� ���� UI ü�¹� ǥ��
    /// </summary>
    private void CurrHPToUISlot()
    {
        int hp = DefinitionManager.Instance.iplayerInfo.GetHp();

        for (int i = 0; i < sliders.Length; i++)
        {
            // ���� �����̴��� ��Ÿ���� �ϴ� HP ���� ���
            int sliderHpRangeStart = i * 2;
            int sliderHpRangeEnd = sliderHpRangeStart + 1;
            
            // HP�� �����̴� ������ �ʰ��ϴ� ��� �����̴��� ������ ä��.
            if (hp > sliderHpRangeEnd)
            {
                if (!sliders[i].gameObject.activeSelf)
                    sliders[i].gameObject.SetActive(true);
                sliders[i].value = 1f;
            }
            // HP�� �����̴� ���� ���� �ִ� ���
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
