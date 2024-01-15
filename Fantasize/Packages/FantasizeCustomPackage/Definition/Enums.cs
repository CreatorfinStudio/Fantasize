using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Definition
{
    /// <summary>
    /// 플레이어
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
        Jump,
        Dash,        
        Attack,
        SpecialAttack, //특수공격
        Block,
        BlockSuccess,
        BlockFail,
        //Sit,
        //SitWalk,      
    }

    /// <summary>
    /// 플레이어 공격타입
    /// </summary>
    public enum AttackType
    {
        None = -1,
        Attack,
        SpecialAttack,
        AirAttack,
    }
    public enum PWeaponPosi
    {
        None = -1,
        Attack_L,
        Attack_R,
        SpecialAttack_L,
        SpecialAttack_R,
        AirAttack_L,
        AirAttack_R,
    }
    public enum BlockType
    {
        None = -1,
        BlockSuccess,
        BlockFail,
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
        Idle,
        Roam,
        Attack,
        Die,
    }

}