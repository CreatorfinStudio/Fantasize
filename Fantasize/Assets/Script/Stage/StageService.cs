using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageService : MonoBehaviour
{

    public void LoadStage()
    {
        //나중에 해당 프리팹들에 맞게 로드하는 코드 추가
        SceneManager.LoadScene("RockMan");
    }
}
