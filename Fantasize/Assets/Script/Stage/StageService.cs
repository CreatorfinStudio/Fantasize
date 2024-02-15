using Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Stage
{
    public class StageService : MonoBehaviour, IStageInfo
    {
        //현재 진행중인 스테이지 정보
        public CurrStageInfo currStageInfo;

        [Header("최대 스테이지 개수(임시 10개)")]
        [SerializeField]
        private int totalStageNum; //최대 스테이지 개수. 현재는 임시로 10

        [Header("=========== 전체 게임 스테이지 정보 ===========")]
        public List<StageData> allStagesList = new List<StageData>();

        [Header("현재 스테이지의 전투구역 / UI 링크용")]
        [SerializeField]
        private BattleAreaInfo[] battleAreaInfos;

        #region Const
        //전체 전투구역 개수
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
                //현재 전투구역 클리어 완료 처리 및 다음 스테이지 데이터 셋
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
            //최대 개수만큼 스테이지 생성
            for (int i = 0; i < totalStageNum; i++)
            {
                allStagesList.Add(new StageData(battleAreaInfos, i));
            }
        }

        /// <summary>
        /// 새 스테이지 시작시 모든 전투구역 초기화
        /// </summary>
        private void ResetAllArea()
        {
            for (int i = 0; i < battleAreaInfos.Length; i++)
            {
                battleAreaInfos[i].IsClear = false;
                battleAreaInfos[i].IsCanPlay = true;
            }
            //마지막 구역은 봉쇄
            battleAreaInfos[5].IsCanPlay = false;
        }

        public void ClearBattleZone(int stageNum, int areaNum)
        {
            if (allStagesList.Count == 0)
                return;

            //현재 구역 클리어 처리
            allStagesList[stageNum].BattleAreasInfo[areaNum].IsClear = true;

            //만약 모든 전투구역이 클리어 되었다면 마지막 스테이지 오픈
            //마지막 스테이지가 클리어되었을때는 패스
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
        /// 다음 스테이지로 넘기기
        /// </summary>
        public void OnNextStage()
        {
            //마지막 스테이지가 열려 있거나 마지막 스테이지를 클리어했을 때
            if (allStagesList[currStageInfo.CurrStageNum].IsOpenLastArea || allStagesList[currStageInfo.CurrStageNum].BattleAreasInfo[totalAreaCount - 1].IsClear)
            {
                currStageInfo.CurrStageNum++;               
                GameManager.isStageClear = true;
            }
        }

        /// <summary>
        /// 전투 시작된 구역 넘버 전달
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

        //allStagesList 세팅될때까지 대기
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