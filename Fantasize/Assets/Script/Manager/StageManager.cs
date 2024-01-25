//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;

//namespace Manager
//{
//    public class StageManager : MonoBehaviour
//    {
//        #region Singleton
//        private static StageManager instance;
//        public static StageManager Instance
//        {
//            get
//            {
//                if (instance == null)
//                {
//                    instance = FindObjectOfType<StageManager>();
//                }
//                return instance;
//            }
//        }
//        #endregion
              
//        [Header("최대 스테이지 개수(임시 10개)")]
//        [SerializeField]
//        private int totalStageNum; //최대 스테이지 개수. 현재는 임시로 10

//        [Header("전체 게임 스테이지 정보")]
//        public List<StageData> stages = new List<StageData>();

//        void Start()
//        {
//            InitializeStages();
//        }

//        // 스테이지 데이터 초기화
//        void InitializeStages()
//        {
//            for (int i = 0; i < totalStageNum; i++) // 10개의 스테이지 예시
//            {
//                stages.Add(new StageData(i+1)); 
//            }
//        }

//        // 전투 구역 클리어 처리
//        public void MarkBattleZoneCompleted(int stageNumber, int battleZone)
//        {
//            if (stageNumber < stages.Count && battleZone < stages[stageNumber].BattleAreaCompleted.Length)
//            {
//                stages[stageNumber].BattleAreaCompleted[battleZone-1] = true;
//            }
//        }

//        // 6번째 전투 구역이 열렸는지 확인
//        public bool IsFinalBattleZoneAvailable(int stageNumber)
//        {
//            if (stageNumber < stages.Count)
//            {
//                int completedZones = stages[stageNumber].BattleAreaCompleted.Count(zone => zone);
//                return completedZones >= 5; // 최소 5개의 구역을 완료해야 함
//            }
//            return false;
//        }

//        // 다음 스테이지로 이동 가능한지 확인
//        public bool CanMoveToNextStage(int stageNumber)
//        {
//            if (stageNumber < stages.Count)
//            {
//                return IsFinalBattleZoneAvailable(stageNumber);
//            }
//            return false;
//        }
//    }
//}