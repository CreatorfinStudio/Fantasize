using Manager;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Stage
{
    public class StageUIService : MonoBehaviour
    {
        [Header("���� �������� ǥ��")]
        [SerializeField]
        private TMP_Text currStageNumTxt;
        [SerializeField]
        private Image[] stageSlotImg;

        [Space(5)]
        [Header("Next Stage Button")]
        [SerializeField]
        private Button nextStageBtn;

        //���߿� ������������ ���� �̹����� �ٲ�������� �׶� ����� �߰�
        //����� �������� N �ؽ�Ʈ ��ȯ��ɸ� �������
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
                stageSlotImg[index].color = newColor; // �÷� ����

                // ��ư�� interactable �Ӽ� ����
                Button buttonComponent = stageSlotImg[index].GetComponent<Button>();
                if (buttonComponent != null)
                {
                    buttonComponent.interactable = colorCode != "#494949";
                }
            }
            else
            {
                Debug.LogError("�߸��� �÷� �ڵ�");
            }

            SetLastStageState();
        }

        //�����ʿ� -- ������ ������ ������ �ٽ� ��
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
            Debug.LogError("������������ ����===");

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
        /// �ټ� ������ ��� Ŭ����Ǹ� 6������ ����
        /// </summary>
        public void SetLastStageState()
        {
            //UI �ӽ� ���� ~
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