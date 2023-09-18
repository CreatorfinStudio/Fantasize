using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Definition;
using TMPro;
public class ManagerUseTest : MonoBehaviour
{    
    #region 싱글톤
    private static ManagerUseTest instance;

    // 게임 싱글톤 인스턴스에 접근하는 프로퍼티
    public static ManagerUseTest Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ManagerUseTest>();

                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<ManagerUseTest>();
                    singletonObject.name = "ManagerUseTest";
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return instance;
        }
    }
    #endregion

    public GameObject reloadingTxt;

    protected IPlayerInfo iplayerInfo;
    protected IItemProcessing iItemProcessing;

    [Header("플레이어")]
    public Slider hpSlider;
    public Slider hungrySlider;
    public TMP_Text hpTxt;
    public TMP_Text hungryTxt;
    public TMP_Text plyerInfoTxt;
    [Header("아이템 슬롯")]
    public TMP_Text itemTxt;
    private void Start()
    {
        StartCoroutine(SetInterface());
    }
    private void Update()
    {
        if(iplayerInfo != null)            
        {
            hpSlider.value = iplayerInfo.GetHp() * .01f;
            hungrySlider.value = iplayerInfo.GetHungry() * .01f;
            SetPlayerInfoTxt();
        }
       
    }
    IEnumerator SetInterface()
    {
        while (iplayerInfo == null)
            iplayerInfo = DefinitionManager.Instance.iplayerInfo;
        while (iItemProcessing == null)
            iItemProcessing = DefinitionManager.Instance.iItemProcessing;
        yield return null;
    }
    
    // UI 스탯 표기용
    void SetPlayerInfoTxt()
    {
        plyerInfoTxt.text = (iplayerInfo?.GetWalkSpeed() + "\n" +
                             iplayerInfo?.GetAttackPower() + "\n"+
                             iplayerInfo?.GetAttackSpeed() + "\n" +
                             iplayerInfo?.GetMaxHP() + "\n" +
                             iplayerInfo?.GetMaxHungry() + "\n"      
                             );

        hpTxt.text = iplayerInfo.GetHp().ToString() + " / " + iplayerInfo.GetMaxHP().ToString();
        hungryTxt.text = iplayerInfo.GetHungry().ToString() + " / " + iplayerInfo.GetMaxHungry().ToString();

        itemTxt.text = iItemProcessing?.GetSlotItemName();
    }

}
