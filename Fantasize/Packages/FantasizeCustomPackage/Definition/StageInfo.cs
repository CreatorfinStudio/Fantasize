using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class StageData
{
    [SerializeField]
    private int stageNum;

    [Header("�� ���� ���� ����")]
    [SerializeField]
    private BattleAreaInfo[] battleAreasInfo;

    [Header("������ ���� ���� ����")]
    [SerializeField]
    private bool isOpenLastArea;

    public int StageNum { get { return stageNum; } set { stageNum = value; } }
    public bool IsOpenLastArea { get { return isOpenLastArea; } set { isOpenLastArea = value; } }
    public BattleAreaInfo[] BattleAreasInfo { get { return battleAreasInfo; } set { battleAreasInfo = value; } }

    public StageData(BattleAreaInfo[] battleAreaInfo, int stageNum = 0, bool isOpenLastArea = false)
    {
        this.battleAreasInfo = battleAreaInfo;
        this.stageNum = stageNum;
        this.isOpenLastArea = isOpenLastArea;
    }
}

[System.Serializable]
public struct CurrStageInfo
{
    [Header("���� ��������")]
    [SerializeField]
    private int currStageNum;

    [Header("���� ��������")]
    [SerializeField]
    private int currBattleAreaNum;
    public int CurrStageNum { get { return currStageNum; } set { currStageNum = value; } }
    public int CurrBattleAreaNum { get { return currBattleAreaNum; } set { currBattleAreaNum = value; } }
}

[System.Serializable]
public struct BattleAreaInfo
{
    //�� ���� Ŭ���� ����
    [SerializeField]
    private bool isClear;
    //������ ���� ���� ����
    [SerializeField]
    private bool isCanPlay;

    public bool IsClear { get { return isClear; } set { isClear = value; } }
    public bool IsCanPlay { get { return isCanPlay; } set { isCanPlay = value; } }

}

//�̰� ���� ���� ���ص��Ǵ°Ÿ� �������� �����ϱ�
public class LoadAreaData
{
    public GameObject mapPrefab;
    public GameObject monsterPrefab;
    //public AudioSource bgm;
}