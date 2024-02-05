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
            ONLastStage();

            GameManager.gameClearEvent += SetStageUI;
            GameManager.gameRestartEvent += CheckOnNextStageBtn;
        }
        private void SetStageUI()
        {
            ONLastStage();

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

                // 버튼의 interactable 속성 조정
                Button buttonComponent = stageSlotImg[index].GetComponent<Button>();
                if (buttonComponent != null)
                {
                    buttonComponent.interactable = colorCode != "#494949";
                }
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


        /// <summary>
        /// 5구역까지 클리어되면 6구역이 열림
        /// </summary>
        public void ONLastStage()
        {
            stageSlotImg[5].gameObject.GetComponent<Button>().interactable = GameManager.Instance.iStageInfo.GetCurrStageNum() == 5;
        }
    }
}