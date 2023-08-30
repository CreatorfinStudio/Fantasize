using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

}
