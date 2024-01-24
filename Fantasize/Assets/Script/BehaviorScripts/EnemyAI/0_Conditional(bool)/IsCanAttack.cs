using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using Definition;

namespace AI
{
    public class IsCanAttack : Conditional
    {
        public override TaskStatus OnUpdate()
        {
            if (DefinitionManager.Instance.imonsterInfo != null)
            {
                return DefinitionManager.Instance.imonsterInfo.GetIsCanAttack()
                ? TaskStatus.Success : TaskStatus.Failure;
            }
            return TaskStatus.Running;
        }
    }
}