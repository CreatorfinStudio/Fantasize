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

    public enum ItemInfoTable
    {
        None = -1,
        Name,
        Image,
        Price,
        HP,
        MaxHP,
        AttackDamage,
        AttackSpeed,
        MoveSpeed,
        SpecialAttackDamage,
        CastingSpeed,
        Calculation,
        ItemSource,
        IsLegacy,
        Grade,
        UnlockStage
    }

    ////////////////////// Item //////////////////////

    /// <summary>
    /// 아이템 얻어지는 경로
    /// </summary>
    public enum ItemSource
    {
        None = -1,
        CommonItem,
        ShopItem,
        DropItem,
    }

    /// <summary>
    /// 아이템 연산 타입
    /// </summary>
    public enum ItemCalculation
    {
        None = -1,
        Addition,
        Multiplication
    }

    public enum ItemGrade
    {
        None = -1,
        Common,
        Rare,
        Unique,
        Legend,
    }

}