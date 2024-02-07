using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaService : MonoBehaviour , IAreaInfo
{
    public BattleAreaInfo battleAreaInfo;

    #region interface
    public bool GetIsClear() => battleAreaInfo.IsClear;

    public bool SetIsClear(bool isClear) => battleAreaInfo.IsClear = isClear;

    #endregion
}
