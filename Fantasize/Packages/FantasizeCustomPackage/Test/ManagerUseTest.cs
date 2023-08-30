using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

}
