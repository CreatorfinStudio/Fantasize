using Definition;
using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageService : MonoBehaviour
{
    [Header("���� ������ �˾� �̹��� / �ӽ�")]
    [SerializeField]
    private GameObject battleStartImg;
    public void LoadStage()
    {
        PopStartImg();
        GameManager.gameRestartEvent += () => {battleStartImg.gameObject.SetActive(false);};
    }

    /// <summary>
    /// ���� ������ �̹��� �˾� (�ӽ�)
    /// </summary>
    private void PopStartImg()
    {
        battleStartImg.gameObject.SetActive(true);       
        GameManager.Instance.GameRestart();
    }
}
