using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using Definition;

namespace AI
{
    public class IsTriggerWall : Conditional
    {
        public override TaskStatus OnUpdate()
        {
            return DefinitionManager.Instance.imonsterInfo.GetIsCanRush()
                ? TaskStatus.Failure : TaskStatus.Success;
        }

    }
}