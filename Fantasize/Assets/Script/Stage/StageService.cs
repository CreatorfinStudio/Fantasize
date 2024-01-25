using Manager;
using System.Collections.Generic;
using UnityEngine;

public class StageService : MonoBehaviour, IStageInfo
{
    [Header("최대 스테이지 개수(임시 10개)")]
    [SerializeField]
    private int totalStageNum; //최대 스테이지 개수. 현재는 임시로 10

    [Header("전체 게임 스테이지 정보")]
    public List<StageData> allStagesList = new List<StageData>();

    public CurrStageInfo currStageInfo;


    private void Awake()
    {
        //게임 클리어시마다 전투구역 ++
        GameManager.gameClearEvent += () =>
        {
            //현재 전투구역 클리어 완료 처리 및 다음 스테이지 데이터 셋
            ClearBattleZone(currStageInfo.CurrStageNum, currStageInfo.CurrBattleAreaNum);
        };

    }
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        //최대 개수만큼 스테이지 생성
        for (int i = 0; i < totalStageNum; i++)
        {
            allStagesList.Add(new StageData(i));
        }

        //아예 게임 첫 실행이면 1
        if (currStageInfo.CurrStageNum < 1)
            currStageInfo.CurrStageNum = 0;
        if (currStageInfo.CurrBattleAreaNum < 1)
            currStageInfo.CurrBattleAreaNum = 0;

    }

    public void ClearBattleZone(int stageNum, int areaNum)
    {
        if (allStagesList.Count == 0)
            return;

        //현재 구역 클리어 처리
        allStagesList[stageNum].BattleAreaCompleted[areaNum] = true;

        //지금 마지막 전투구역(6)이었다면 스테이지 넘기고 구역 초기화
        if (currStageInfo.CurrBattleAreaNum > 5)
        {
            currStageInfo.CurrStageNum++;
            currStageInfo.CurrBattleAreaNum = 0;
        }
        else
            currStageInfo.CurrBattleAreaNum++;
    }

    /// <summary>
    /// 다음 스테이지로 넘기기
    /// </summary>
    public void NextStage()
    {
        currStageInfo.CurrStageNum++;
        currStageInfo.CurrBattleAreaNum = 0;
    }

    #region Interface

    //  currStageInfo.CurrBattleAreaNum++가 먼저 처리되고 오기때문에 -1 해줌.
    public (int, bool) GetIsBattleAreaCompleted()
    {
        return (currStageInfo.CurrBattleAreaNum-1, allStagesList[currStageInfo.CurrStageNum].BattleAreaCompleted[currStageInfo.CurrBattleAreaNum-1]);
    }

    public int GetCurrStageNum() => currStageInfo.CurrStageNum;

    public int GetCurrBattleAreaNum() => currStageInfo.CurrBattleAreaNum;

    #endregion
}