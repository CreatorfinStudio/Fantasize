using Definition;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Slider[] sliders;

    private void Update()
    {
        CurrHPToUISlot();
    }

    /// <summary>
    /// 현재 HP 상태에 따른 UI 체력바 표기
    /// </summary>
    private void CurrHPToUISlot()
    {
        int hp = DefinitionManager.Instance.iplayerInfo.GetHp();

        if(hp > 0 && hp <= 2) 
        {
            IsHalfHp(0);
            sliders[1].value = 0f;
            sliders[2].value = 0f;
        }
        else if(hp > 2 && hp <= 4) 
        {
            IsHalfHp(1);
            sliders[2].value = 0f;
        }
        else if(hp > 4 && hp <= 6) 
        {
            IsHalfHp(2);
        }
        else if (hp == 0)
        {
            sliders[0].value = 0f;
            sliders[1].value = 0f;
            sliders[2].value = 0f;
        }

        void IsHalfHp(int num)
        {
            if (hp % 2 != 0)
                sliders[num].value = .5f;
            else
                sliders[num].value = 1f;
        }

    }
}
