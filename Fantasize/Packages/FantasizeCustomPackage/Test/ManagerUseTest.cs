using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Definition;

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
    [Header("플레이어")]
    public Slider hpSlider;
    public Slider hungrySlider;
    private void Start()
    {
        StartCoroutine(SetIPlayerInfo());
    }
    private void Update()
    {
        if(iplayerInfo != null)            
        {
            hpSlider.value = iplayerInfo.GetHp() * .01f;
            hungrySlider.value = iplayerInfo.GetHungry() * .01f;
        }
    }
    IEnumerator SetIPlayerInfo()
    {
        while (iplayerInfo == null)
            iplayerInfo = DefinitionManager.Instance.iplayerInfo;
        yield return null;
    }
}
