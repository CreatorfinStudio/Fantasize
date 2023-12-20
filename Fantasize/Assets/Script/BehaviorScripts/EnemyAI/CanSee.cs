using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using Definition;

namespace AI
{
    public class CanSee : Conditional
    {      

        public override TaskStatus OnUpdate()
        {
            return DefinitionManager.Instance.imonsterInfo.GetCanSeePlayer() 
                ? TaskStatus.Success : TaskStatus.Failure;
        }


    }
}