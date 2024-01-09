using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using Definition;

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

            //입력이 없더라도 지금 대시 상태면 성공 반환
            if(DefinitionManager.Instance.iplayerInfo.GetIsDashing())
            {
                return TaskStatus.Success;
            }

            return TaskStatus.Failure;

        }
    }

}