using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageService : MonoBehaviour
{

    public void LoadStage()
    {
        //���߿� �ش� �����յ鿡 �°� �ε��ϴ� �ڵ� �߰�
        SceneManager.LoadScene("RockMan");
    }
}
