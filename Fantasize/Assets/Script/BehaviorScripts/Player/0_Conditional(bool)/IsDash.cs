using BehaviorDesigner.Runtime.Tasks;
using Definition;
using System;
using UnityEngine;

namespace AI
{
    public class IsDash : Conditional
    {
        private float doubleClickTimeThreshold = 0.3f;
        private float lastLeftClickTime = -1f;
        private float lastRightClickTime = -1f;

        public override TaskStatus OnUpdate()
        {
            float currentTime = Time.time;

            try
            {
                if (DefinitionManager.Instance.iplayerInfo.GetIsDashing())
                {
                    return TaskStatus.Failure;
                }
            }
            catch (NullReferenceException ex)
            {
                //iplayerInfo가 일시적으로 null이긴 하지만 노상관
            }


            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (lastLeftClickTime > 0 && (currentTime - lastLeftClickTime) <= doubleClickTimeThreshold)
                {
                    DefinitionManager.Instance.iplayerInfo.SetDashDirection(-1);
                    return TaskStatus.Success;
                }
                lastLeftClickTime = currentTime;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (lastRightClickTime > 0 && (currentTime - lastRightClickTime) <= doubleClickTimeThreshold)
                {
                    DefinitionManager.Instance.iplayerInfo.SetDashDirection(1);
                    return TaskStatus.Success;
                }
                lastRightClickTime = currentTime;
            }

            return TaskStatus.Failure;
        }
    }

}