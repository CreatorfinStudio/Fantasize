using Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage
{
    public class StageService : MonoBehaviour, IStageInfo
    {
        //���� �������� �������� ����
        public CurrStageInfo currStageInfo;

        [Header("�ִ� �������� ����(�ӽ� 10��)")]
        [SerializeField]
        private int totalStageNum; //�ִ� �������� ����. ����� �ӽ÷� 10

        [Header("=========== ��ü ���� �������� ���� ===========")]
        public List<StageData> allStagesList = new List<StageData>();

        [Header("���� ���������� �������� / UI ��ũ��")]
        [SerializeField]
        private BattleAreaInfo[] battleAreaInfos;

        #region Const
        //��ü �������� ����
        public const int totalAreaCount = 6;

        #endregion

        public void TEST()
        {
            for(int i = 0; i < totalAreaCount -1; i++)
            {
                allStagesList[currStageInfo.CurrStageNum].BattleAreasInfo[i].IsClear = true;
            }
            allStagesList[currStageInfo.CurrStageNum].IsOpenLastArea = true;
        }

        private void Awake()
        {
            ResetAllArea();

            GameManager.areaClearEvent += () =>
            {
                //���� �������� Ŭ���� �Ϸ� ó�� �� ���� �������� ������ ��
                ClearBattleZone(currStageInfo.CurrStageNum, currStageInfo.CurrBattleAreaNum);
            };
            GameManager.stageClearEvent += ResetAllArea;            
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
                allStagesList.Add(new StageData(battleAreaInfos, i));
            }
        }

        /// <summary>
        /// �� �������� ���۽� ��� �������� �ʱ�ȭ
        /// </summary>
        private void ResetAllArea()
        {
            for (int i = 0; i < battleAreaInfos.Length; i++)
            {
                battleAreaInfos[i].IsClear = false;
                battleAreaInfos[i].IsCanPlay = true;
            }
            //������ ������ ����
            battleAreaInfos[5].IsCanPlay = false;
        }

        public void ClearBattleZone(int stageNum, int areaNum)
        {
            if (allStagesList.Count == 0)
                return;

            //���� ���� Ŭ���� ó��
            allStagesList[stageNum].BattleAreasInfo[areaNum].IsClear = true;

            //���� ��� ���������� Ŭ���� �Ǿ��ٸ� ������ �������� ����
            //������ ���������� Ŭ����Ǿ������� �н�
            if (!allStagesList[currStageInfo.CurrStageNum].BattleAreasInfo[totalAreaCount - 1].IsClear)
            {
                bool allClear = true;

                for (int i = 0; i < totalAreaCount - 1; i++)
                {
                    if (!battleAreaInfos[i].IsClear)
                    {
                        allClear = false;
                        break;
                    }
                }

                if (allClear)
                {
                    allStagesList[stageNum].IsOpenLastArea = true;
                }
            }
        }

        /// <summary>
        /// ���� ���������� �ѱ��
        /// </summary>
        public void OnNextStage()
        {
            //������ ���������� ���� �ְų� ������ ���������� Ŭ�������� ��
            if (allStagesList[currStageInfo.CurrStageNum].IsOpenLastArea || allStagesList[currStageInfo.CurrStageNum].BattleAreasInfo[totalAreaCount - 1].IsClear)
            {
                currStageInfo.CurrStageNum++;               
                GameManager.isStageClear = true;
            }
        }

        /// <summary>
        /// ���� ���۵� ���� �ѹ� ����
        /// </summary>
        public void SetCurrBattleAreaNum(GameObject clickedButton)
        {
            for (int i = 1; i <= totalAreaCount; i++)
            {
                if (clickedButton.name.Contains(i.ToString()))
                {
                    currStageInfo.CurrBattleAreaNum = i - 1;
                    break;
                }
            }
        }


        #region Interface
        public (int, bool) GetIsBattleAreaCompleted()
        {
            return (currStageInfo.CurrBattleAreaNum, allStagesList[currStageInfo.CurrStageNum].BattleAreasInfo[currStageInfo.CurrBattleAreaNum].IsClear);
        }
        public void GetIsOpenLastArea(Action<bool> resultCallback)
        {
            StartCoroutine(WaitSetAllStagesList(resultCallback));
        }

        //allStagesList ���õɶ����� ���
        IEnumerator WaitSetAllStagesList(Action<bool> resultCallback)
        {
            while (allStagesList.Count == 0)
                yield return null;

            resultCallback?.Invoke(allStagesList[currStageInfo.CurrStageNum].IsOpenLastArea);
        }

        public int GetCurrStageNum() => currStageInfo.CurrStageNum;

        public int GetCurrBattleAreaNum() => currStageInfo.CurrBattleAreaNum;

        #endregion
    }
}