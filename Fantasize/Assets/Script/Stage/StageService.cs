using Manager;
using System.Collections.Generic;
using UnityEngine;

public class StageService : MonoBehaviour, IStageInfo
{
    [Header("�ִ� �������� ����(�ӽ� 10��)")]
    [SerializeField]
    private int totalStageNum; //�ִ� �������� ����. ����� �ӽ÷� 10

    [Header("��ü ���� �������� ����")]
    public List<StageData> allStagesList = new List<StageData>();

    public CurrStageInfo currStageInfo;


    private void Awake()
    {
        //���� Ŭ����ø��� �������� ++
        GameManager.gameClearEvent += () =>
        {
            //���� �������� Ŭ���� �Ϸ� ó�� �� ���� �������� ������ ��
            ClearBattleZone(currStageInfo.CurrStageNum, currStageInfo.CurrBattleAreaNum);
        };

    }
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        //�ִ� ������ŭ �������� ����
        for (int i = 0; i < totalStageNum; i++)
        {
            allStagesList.Add(new StageData(i));
        }

        //�ƿ� ���� ù �����̸� 1
        if (currStageInfo.CurrStageNum < 1)
            currStageInfo.CurrStageNum = 0;
        if (currStageInfo.CurrBattleAreaNum < 1)
            currStageInfo.CurrBattleAreaNum = 0;

    }

    public void ClearBattleZone(int stageNum, int areaNum)
    {
        if (allStagesList.Count == 0)
            return;

        //���� ���� Ŭ���� ó��
        allStagesList[stageNum].BattleAreaCompleted[areaNum] = true;

        //���� ������ ��������(6)�̾��ٸ� �������� �ѱ�� ���� �ʱ�ȭ
        if (currStageInfo.CurrBattleAreaNum > 5)
        {
            currStageInfo.CurrStageNum++;
            currStageInfo.CurrBattleAreaNum = 0;
        }
        else
            currStageInfo.CurrBattleAreaNum++;
    }

    /// <summary>
    /// ���� ���������� �ѱ��
    /// </summary>
    public void NextStage()
    {
        currStageInfo.CurrStageNum++;
        currStageInfo.CurrBattleAreaNum = 0;
    }

    #region Interface

    //  currStageInfo.CurrBattleAreaNum++�� ���� ó���ǰ� ���⶧���� -1 ����.
    public (int, bool) GetIsBattleAreaCompleted()
    {
        return (currStageInfo.CurrBattleAreaNum-1, allStagesList[currStageInfo.CurrStageNum].BattleAreaCompleted[currStageInfo.CurrBattleAreaNum-1]);
    }

    public int GetCurrStageNum() => currStageInfo.CurrStageNum;

    public int GetCurrBattleAreaNum() => currStageInfo.CurrBattleAreaNum;

    #endregion
}