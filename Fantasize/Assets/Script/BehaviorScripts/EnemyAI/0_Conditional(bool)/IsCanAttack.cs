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
            return DefinitionManager.Instance.imonsterInfo.GetIsCanAttack()
                ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}