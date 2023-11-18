using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Definition
{
    /// <summary>
    /// �÷��̾�
    /// </summary>
    public enum PlayerState
    {
        None = -1,
        Idle,
        //Walk,
        //Jump,
        //WalkJump,
        //BackWalk,
        Run,
        RunStop,
        RunJump,
        Jump,
        Dash,        
        Attack,
        SpecialAttack,
        Block,
        BlockSucess,
        BlockFail,
        //Sit,
        //SitWalk,      
    }


    /// <summary>
    /// ���� ����
    /// </summary>
    public enum MonsterType
    {
        None = -1,
        Small,
        Medium,
        Boss,
    }

    /// <summary>
    /// ���� State
    /// </summary>
    public enum MonterState
    {
        None = -1,
        Idle,
        Roam,
        Attack,
        Die,
    }
}