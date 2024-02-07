using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class StageData
{
    [SerializeField]
    private int stageNum;

    [Header("각 전투 구역 정보")]
    [SerializeField]
    private BattleAreaInfo[] battleAreasInfo;

    [Header("마지막 구역 개방 여부")]
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
    [Header("현재 스테이지")]
    [SerializeField]
    private int currStageNum;

    [Header("현재 전투구역")]
    [SerializeField]
    private int currBattleAreaNum;
    public int CurrStageNum { get { return currStageNum; } set { currStageNum = value; } }
    public int CurrBattleAreaNum { get { return currBattleAreaNum; } set { currBattleAreaNum = value; } }
}

[System.Serializable]
public struct BattleAreaInfo
{
    //이 구역 클리어 여부
    [SerializeField]
    private bool isClear;
    //마지막 구역 개방 여부
    [SerializeField]
    private bool isCanPlay;

    public bool IsClear { get { return isClear; } set { isClear = value; } }
    public bool IsCanPlay { get { return isCanPlay; } set { isCanPlay = value; } }

}

//이건 굳이 따로 안해도되는거면 위에꺼랑 병합하기
public class LoadAreaData
{
    public GameObject mapPrefab;
    public GameObject monsterPrefab;
    //public AudioSource bgm;
}