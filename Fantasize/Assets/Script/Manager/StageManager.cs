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
              
//        [Header("�ִ� �������� ����(�ӽ� 10��)")]
//        [SerializeField]
//        private int totalStageNum; //�ִ� �������� ����. ����� �ӽ÷� 10

//        [Header("��ü ���� �������� ����")]
//        public List<StageData> stages = new List<StageData>();

//        void Start()
//        {
//            InitializeStages();
//        }

//        // �������� ������ �ʱ�ȭ
//        void InitializeStages()
//        {
//            for (int i = 0; i < totalStageNum; i++) // 10���� �������� ����
//            {
//                stages.Add(new StageData(i+1)); 
//            }
//        }

//        // ���� ���� Ŭ���� ó��
//        public void MarkBattleZoneCompleted(int stageNumber, int battleZone)
//        {
//            if (stageNumber < stages.Count && battleZone < stages[stageNumber].BattleAreaCompleted.Length)
//            {
//                stages[stageNumber].BattleAreaCompleted[battleZone-1] = true;
//            }
//        }

//        // 6��° ���� ������ ���ȴ��� Ȯ��
//        public bool IsFinalBattleZoneAvailable(int stageNumber)
//        {
//            if (stageNumber < stages.Count)
//            {
//                int completedZones = stages[stageNumber].BattleAreaCompleted.Count(zone => zone);
//                return completedZones >= 5; // �ּ� 5���� ������ �Ϸ��ؾ� ��
//            }
//            return false;
//        }

//        // ���� ���������� �̵� �������� Ȯ��
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