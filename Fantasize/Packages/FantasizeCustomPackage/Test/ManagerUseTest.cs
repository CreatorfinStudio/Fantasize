using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Definition;

public class ManagerUseTest : MonoBehaviour
{
    
    #region �̱���
    private static ManagerUseTest instance;

    // ���� �̱��� �ν��Ͻ��� �����ϴ� ������Ƽ
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
    [Header("�÷��̾�")]
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
