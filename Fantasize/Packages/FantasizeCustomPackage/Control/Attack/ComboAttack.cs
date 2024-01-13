using System.Collections;
using UnityEngine;
using Definition;

namespace Control
{
    public class ComboAttack : Attack
    {
        public float comboTimeThreshold = 2f;

        //���߿� Definition�� ������ ������ ��
        public float reloadTime = 2f;
        public int maxComboAttacks = 3;
        public enum AttackState
        {
            Idle,
            ComboAvailable,
            Reloading
        }

        public AttackState currentState = AttackState.ComboAvailable;
        private int comboCount = 0;
        private float lastComboTime = 0f;

        private bool reloadingStart = false;
        private void Update()
        {
            switch (currentState)
            {
                case AttackState.ComboAvailable:
                    //���콺�� Ŭ��������
                    if (Input.GetMouseButtonDown(0) && !isLongClicking)
                    {
                        //ù �����̰ų�, ���� ���� �� 2�ʰ� ������ �ʾ����� (�޺� ������ �����϶�)
                        if (comboCount == 0 || IsWithinComboTime())
                        {
                            if (Input.GetMouseButtonDown(0))
                            {
                                StartComboAvailable();
                            }
                        }
                        else
                        {
                            StartReloading();
                        }
                    }
                    else if (!(comboCount == 0) && !IsWithinComboTime())
                    {
                        StartReloading();
                    }
                    break;

                case AttackState.Reloading:
                    //���ε� ���� �ƴҶ��� ���ε� ���� ����
                    if (!reloadingStart)
                        StartCoroutine(ReloadCountdown());
                    break;
            }
        }

        private bool IsWithinComboTime()
        {
            return Time.time - lastComboTime <= comboTimeThreshold;
        }

        private void StartComboAvailable()
        {
            //���� Ŭ���� ���� ����
            lastComboTime = Time.time;
            //�޺� �ִϸ��̼� ����
            //PlayComboAnimation(comboCount);
            //���� �޺� ī��Ʈ
            comboCount++;

            //���� ���� �޺��� �޺������� ������ �������
            if (comboCount >= maxComboAttacks)
            {
                //���ε� ���·� ��ȯ
                StartReloading();
            }
        }

        private void StartReloading()
        {
            currentState = AttackState.Reloading;
        }

        private IEnumerator ReloadCountdown()//Action action)
        {
            ManagerUseTest.Instance.reloadingTxt?.SetActive(true);
            reloadingStart = true;
            yield return new WaitForSeconds(reloadTime);
            comboCount = 0;

            currentState = AttackState.ComboAvailable;

            ManagerUseTest.Instance.reloadingTxt?.SetActive(false);
            reloadingStart = false;

        }
        //private void PlayComboAnimation(int _comboCount)
        //{
        //    switch (_comboCount)
        //    {
        //        case 0:
        //            //Debug.Log("���� 1");
        //            AnimationManager.TriggerAnim(animator, "Attack1");
        //            break;
        //        case 1:
        //            // Debug.Log("���� 2");
        //            AnimationManager.TriggerAnim(animator, "Attack2");
        //            break;
        //        case 2:
        //            // Debug.Log("���� 3");
        //            AnimationManager.TriggerAnim(animator, "Attack3");
        //            break;
        //    }
        //}
    }
}