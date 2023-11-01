using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Definition
{
    /// <summary>
    /// 플레이어
    /// </summary>
    public enum PlayerMove
    {
        None = -1,
        Idle,
        Walk,
        //Jump,
        WalkJump,
        BackWalk,
        Run,
        RunStop,
        RunJump,
        Sit,
        SitWalk,
      //  Attack, //이거는 Move쪽보다는 다른곳으로 가야할듯. 현재는 Combo 스크립트에도 있다.
    }


    /// <summary>
    /// 몬스터 종류
    /// </summary>
    public enum MonsterType
    {
        None = -1,
        Small,
        Medium,
        Boss,
    }

    /// <summary>
    /// 몬스터 State
    /// </summary>
    public enum MonterState
    {
        None = -1,
        Roam,
        Attack,
        Die,
    }
}