using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Definition;

namespace Control
{   

    public class Controller : MonoBehaviour
    {
        protected IPlayerInfo iplayerInfo;
        protected bool isLongClicking = false;


        protected virtual void Start()
        {
            StartCoroutine(SetIPlayerInfo());
        }
        protected virtual IEnumerator SetIPlayerInfo()
        {
            while (iplayerInfo == null)
                iplayerInfo = DefinitionManager.Instance.iplayerInfo;
            yield return null;
        }
    }
}