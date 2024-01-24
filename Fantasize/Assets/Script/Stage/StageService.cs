using Definition;
using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageService : MonoBehaviour
{
    [Header("전투 시작전 팝업 이미지 / 임시")]
    [SerializeField]
    private GameObject battleStartImg;
    public void LoadStage()
    {
        PopStartImg();
        GameManager.gameRestartEvent += () => {battleStartImg.gameObject.SetActive(false);};
    }

    /// <summary>
    /// 전투 시작전 이미지 팝업 (임시)
    /// </summary>
    private void PopStartImg()
    {
        battleStartImg.gameObject.SetActive(true);       
        GameManager.Instance.GameRestart();
    }
}
