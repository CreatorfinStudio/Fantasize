using Manager;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Stage
{
    public class StageUIService : MonoBehaviour
    {
        [Header("현재 스테이지 표시")]
        [SerializeField]
        private TMP_Text currStageNumTxt;
        [SerializeField]
        private Image[] stageSlotImg;

        [Space(5)]
        [Header("Next Stage Button")]
        [SerializeField]
        private Button nextStageBtn;

        //나중에 스테이지별로 구역 이미지가 바뀔수도있음 그때 여기다 추가
        //현재는 스테이지 N 텍스트 변환기능만 들어있음
        public static Action setStageUIEvent;


        private void Start()
        {
            SetLastStageState();

            GameManager.areaClearEvent += SetStageUI;
            GameManager.areaResetEvent += CheckOnNextStageBtn;
            GameManager.stageClearEvent += NextStage;
            setStageUIEvent += () => currStageNumTxt.text = "Stage " + (GameManager.Instance.iStageInfo.GetCurrStageNum() + 1).ToString();
            setStageUIEvent?.Invoke();
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

            SetLastStageState();
        }

        //수정필요 -- 샵에서 아이템 누르면 다시 뜸
        private void CheckOnNextStageBtn()
        {
            if (nextStageBtn != null && this != null)
            {
                GameManager.Instance.iStageInfo.GetIsOpenLastArea(isopen =>
                {
                    nextStageBtn.interactable = isopen;
                });
            }
        }

        public void NextStage()
        {
            if (stageSlotImg[0] == null)
                return;
            Debug.LogError("다음스테이지 진입===");

            for (int i = 0; i < stageSlotImg.Length; i++)
            {
                if (ColorUtility.TryParseHtmlString("#FFFFFFFF", out Color newColor))
                {
                    stageSlotImg[i].color = newColor;
                    stageSlotImg[i].GetComponent<Button>().interactable = true;
                    if (i == stageSlotImg.Length - 1)
                        stageSlotImg[i].GetComponent<Button>().interactable = false;
                }
            }
            SetLastStageState();
            setStageUIEvent?.Invoke();
        }


        /// <summary>
        /// 다섯 구역이 모두 클리어되면 6구역이 열림
        /// </summary>
        public void SetLastStageState()
        {
            //UI 임시 더미 ~
            Color newColor;

            GameManager.Instance.iStageInfo.GetIsOpenLastArea(isopen =>
            {
                stageSlotImg[5].gameObject.GetComponent<Button>().interactable = isopen;

                if (isopen)
                {
                    if (ColorUtility.TryParseHtmlString("#FFFFFFFF", out newColor))
                    {
                        stageSlotImg[5].color = newColor;
                    }
                }
                else
                {
                    if (ColorUtility.TryParseHtmlString("#494949", out newColor)) 
                    {
                        stageSlotImg[5].color = newColor;
                    }
                }
            });
        }
    }
}