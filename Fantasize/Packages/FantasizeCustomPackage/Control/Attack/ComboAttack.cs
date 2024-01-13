using System.Collections;
using UnityEngine;
using Definition;

namespace Control
{
    public class ComboAttack : Attack
    {
        public float comboTimeThreshold = 2f;

        //나중에 Definition의 값으로 변경할 것
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
                    //마우스를 클릭했을때
                    if (Input.GetMouseButtonDown(0) && !isLongClicking)
                    {
                        //첫 공격이거나, 이전 공격 후 2초가 지나지 않았을때 (콤보 가능한 상태일때)
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
                    //리로딩 중이 아닐때만 리로딩 모드로 진입
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
            //현재 클릭한 시점 저장
            lastComboTime = Time.time;
            //콤보 애니메이션 실행
            //PlayComboAnimation(comboCount);
            //다음 콤보 카운트
            comboCount++;

            //만약 다음 콤보가 콤보가능한 범위를 벗어났을때
            if (comboCount >= maxComboAttacks)
            {
                //리로딩 상태로 전환
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
        //            //Debug.Log("공격 1");
        //            AnimationManager.TriggerAnim(animator, "Attack1");
        //            break;
        //        case 1:
        //            // Debug.Log("공격 2");
        //            AnimationManager.TriggerAnim(animator, "Attack2");
        //            break;
        //        case 2:
        //            // Debug.Log("공격 3");
        //            AnimationManager.TriggerAnim(animator, "Attack3");
        //            break;
        //    }
        //}
    }
}