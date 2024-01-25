using UnityEngine;

public class LoadAreaData
{
    public GameObject mapPrefab;
    public GameObject monsterPrefab;
    //public AudioSource bgm;
}

[System.Serializable]
public struct StageData
{
    [SerializeField]
    private int stageNum;

    [Header("각 전투 구역의 클리어 여부")]
    [SerializeField]
    private bool[] battleAreaCompleted;

    public int StageNum { get { return stageNum; } set { stageNum = value; } }
    public bool[] BattleAreaCompleted { get { return battleAreaCompleted; } set { battleAreaCompleted = value; } }


    public StageData(int stageNum = 0, int battleAreaSize = 6)
    {
        this.stageNum = stageNum;
        this.battleAreaCompleted = new bool[battleAreaSize]; //무조건 6개의 전투 구역
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
