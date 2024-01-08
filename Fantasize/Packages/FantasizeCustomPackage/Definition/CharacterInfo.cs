using System;
using MonsterLove.StateMachine;
using UnityEditor;
using UnityEngine;

namespace Definition
{
    public class CharacterProperty 
    {
        public int hp;
        public float walkSpeed;
        public float attackPower;
        public float specialAttackPower;
        public float attackSpeed;
        public float jumpForce;

        // 0905 기획에서 마우스 회전 관련 삭제됨
        // public float rotationSpeed; 

        /// <summary>
        /// 스킬 재사용 대기시간
        /// </summary>
        [Header("스킬 재사용 대기시간")]
        public float reloadTime; 

        #region Property        
        public int Hp { get { return hp; } set { hp = value; } }
        public float WalkSpeed { get { return walkSpeed; } set { walkSpeed = value; } }
        public float AttackPower { get { return attackPower; } set { attackPower = value; } }
        public float AttackSpeed { get { return attackSpeed; } set { attackSpeed = value; } }
        public float JumpForce { get { return jumpForce; } set { jumpForce = value; } }
        // 0905 기획에서 마우스 회전 관련 삭제됨
       // public float RotationSpeed { get { return rotationSpeed; } set { rotationSpeed = value; } }

        #endregion

        public CharacterProperty() { }
        public CharacterProperty(int hp, float moveSpeed, float attackPower, float attackSpeed)
        {
            this.hp = hp;
            this.walkSpeed = moveSpeed;
            this.attackPower = attackPower;
            this.attackSpeed = attackSpeed;
        }
    }

    [System.Serializable]
    public class PlayerInfo : CharacterProperty
    {
        [Header("Behavior")]
        public bool isDashing;
        public float dashDirection;
        public AttackType attackType;
        /// <summary>
        /// Move FSM
        /// </summary>
        [Space(10)]
        [Header("============== Move FSM ==============")]
        public PlayerState moveFSM;
        [Header("============= Battle Status =============")]
        public float airAttackPower;

        [Space(10)]
        public int hungry;
        public float rangedView;
        public float forwardView;

        [Space(10)]
        public float dashSpeed;

        public float runSpeed;
        public float runJumpForce;

        public int maxHungry;
        public int maxHP;

        #region Property
        public bool IsDashing { get { return isDashing; } set { isDashing = value; } }
        public float DashDirection { get { return dashDirection; } set { dashDirection = value; } }
        public AttackType AttackType { get { return attackType; } set { attackType = value; } }
        
 
        public PlayerState MOVEFSM { get { return moveFSM; } set { moveFSM = value; } }
        public float AirAttackPower { get { return airAttackPower; } set { airAttackPower = value; } }

        public int Hungry { get { return hungry; } set { hungry = value; } }
        public float RangedView { get { return rangedView; } set { rangedView = value; } }
        public float ForwardView { get { return forwardView; } set { forwardView = value; } }
        public float DashSpeed { get { return dashSpeed; } set { dashSpeed = value; } }

        public float RunSpeed { get { return runSpeed; } set { runSpeed = value; } }
        public float RunJumpForce { get { return runJumpForce; } set { runJumpForce = value; } }
        public int MaxHungry { get { return maxHungry; } set { maxHungry = value; } }
        public int MaxHP { get { return maxHP; } set { maxHP = value; } }
    

        #endregion

        public PlayerInfo(int hungry, float rangedView, float forwardView, int maxHungry, int maxHP)
        {
            this.hungry = hungry;
            this.rangedView = rangedView;
            this.ForwardView = forwardView;
            this.Hungry = maxHungry;
            this.maxHP = maxHP;
        }
    }

    [System.Serializable]
    public class MonsterInfo : CharacterProperty
    {
        /// <summary>
        /// 플레이어가 인식 되었는지 여부
        /// </summary>
        [Header("플레이어 인식 여부")]
        public bool canSeePlayer;
        /// <summary>
        /// 플레이어가 충돌 되었는지 여부
        /// </summary>
        [Header("플레이어 충돌 여부")]
        public bool isCollisionPlayer;
        /// <summary>
        /// 특정 범위 내로 들어오면 플레이어에게 공격 시전
        /// </summary>
        [Header("플레이어 공격 가능/시작")]
        public bool isCanAttack;

        /// <summary>
        /// 추적 속도
        /// </summary>
        [Header ("추적 속도")]
        public float followSpeed;   
        
        [Header("돌진 속도")]
        public float rushSpeed;
        /// <summary>
        /// 돌진 가능 여부
        /// </summary>
        public bool isCanRush;
        [Header("실시간 스프라이트 반전시킬건지")]
        public bool isSpriteCheck;


        /// <summary>
        /// 쿨타임 체크용 State Enum
        /// 수정해야 할수도 있음
        /// </summary>
        [Header("이 몬스터의 현재 상태")]
        public MonterState attackState;
        /// <summary>
        /// 어떤 종류의 몬스터인지
        /// </summary>
        public MonsterType monsterType;

        #region Property
        public bool CanSeePlayer { get { return canSeePlayer; } set { canSeePlayer = value; } }
        public bool IsCollisionPlayer { get { return isCollisionPlayer; } set { isCollisionPlayer = value; } }
        public float FollowSpeed { get { return followSpeed; } set { followSpeed = value; } }
        public bool IsCanAttack { get { return isCanAttack; } set { isCanAttack = value; } }
        public float RushSpeed { get { return rushSpeed; } set { rushSpeed = value; } }
        public bool IsCanRush { get { return isCanRush; } set { isCanRush = value; } }
        public bool IsSpriteCheck { get { return isSpriteCheck; } set { isSpriteCheck = value; } }
        public MonterState AtackState { get {  return attackState; } set {  attackState = value; } }
        public MonsterType MonsterType { get {  return monsterType; } set {  monsterType = value; } }
        #endregion
    }
    ////승원이 테스트 윈도우용
    //public struct TestWindowProperty
    //{
    //    private float moveSpeed;
    //    private float rotationSpeed;
    //    private int maxComboAttacks;

    //    public float MoveSpeed
    //    {
    //        get { return moveSpeed; }
    //        set
    //        {
    //            moveSpeed = value;
    //            Test_ChangedMoveSpeed?.Invoke(value);
    //        }
    //    }
    //    public float RotationSpeed
    //    {
    //        get { return rotationSpeed; }
    //        set
    //        {
    //            rotationSpeed = value;
    //            Test_ChangedRotationSpeed?.Invoke(value);
    //        }
    //    }
    //    public int MaxComboAttacks
    //    {
    //        get { return maxComboAttacks; }
    //        set
    //        {
    //            maxComboAttacks = value;
    //            Test_ChangeMaxComboAttacks?.Invoke(value);
    //        }
    //    }

    //    public event Action<float> Test_ChangedMoveSpeed;
    //    public event Action<float> Test_ChangedRotationSpeed;
    //    public event Action<int> Test_ChangeMaxComboAttacks;
    //}
}