using Manager;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Stage
{
    public class StageUIService : MonoBehaviour
    {
        [SerializeField]
        private Image[] stageSlotImg;

        [Space(5)]
        [Header("Next Stage Button")]
        [SerializeField]
        private GameObject nextStageBtn;

        private void Start()
        {
            GameManager.gameClearEvent += SetStageUI;
            GameManager.gameRestartEvent += CheckOnNextStageBtn;
        }
        private void SetStageUI()
        {
            if (GameManager.Instance.iStageInfo == null)
                return;

            int index = GameManager.Instance.iStageInfo.GetIsBattleAreaCompleted().Item1;

            if (index < 0 || index >= stageSlotImg.Length || stageSlotImg[index] == null)
            {                
                return;
            }

            string colorCode = GameManager.Instance.iStageInfo.GetIsBattleAreaCompleted().Item2 ? "#494949" : "#FFFFFFFF";

            if (ColorUtility.TryParseHtmlString(colorCode, out Color newColor))
            {
                stageSlotImg[index].color = newColor; // 컬러 적용
            }
            else
            {
                Debug.LogError("잘못된 컬러 코드");
            }
        }

        private void CheckOnNextStageBtn()
        {
            if (nextStageBtn != null && this != null)
            {
                if (GameManager.Instance.iStageInfo.GetCurrBattleAreaNum() == 5)
                    nextStageBtn.SetActive(true);
                else
                    nextStageBtn.SetActive(false);
            }
        }

        public void NextStage()
        {
            GameManager.Instance.iStageInfo.NextStage();
            for (int i = 0; i < stageSlotImg.Length; i++)
            {
                if (ColorUtility.TryParseHtmlString("#FFFFFFFF", out Color newColor))
                {
                    stageSlotImg[i].color = newColor;
                }
            }
        }

    }
}