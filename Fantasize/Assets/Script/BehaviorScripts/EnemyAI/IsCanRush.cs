using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using Definition;

namespace AI
{
    public class IsCanRush : Conditional
    {
        public override TaskStatus OnUpdate()
        {
            return DefinitionManager.Instance.imonsterInfo.GetIsCanRush()
                ? TaskStatus.Success : TaskStatus.Failure;
        }

    }
}