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

        // 0905 ��ȹ���� ���콺 ȸ�� ���� ������
        // public float rotationSpeed; 

        /// <summary>
        /// ��ų ���� ���ð�
        /// </summary>
        [Header("��ų ���� ���ð�")]
        public float reloadTime; 

        #region Property        
        public int Hp { get { return hp; } set { hp = value; } }
        public float WalkSpeed { get { return walkSpeed; } set { walkSpeed = value; } }
        public float AttackPower { get { return attackPower; } set { attackPower = value; } }
        public float AttackSpeed { get { return attackSpeed; } set { attackSpeed = value; } }
        public float JumpForce { get { return jumpForce; } set { jumpForce = value; } }
        // 0905 ��ȹ���� ���콺 ȸ�� ���� ������
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
        /// �÷��̾ �ν� �Ǿ����� ����
        /// </summary>
        [Header("�÷��̾� �ν� ����")]
        public bool canSeePlayer;
        /// <summary>
        /// �÷��̾ �浹 �Ǿ����� ����
        /// </summary>
        [Header("�÷��̾� �浹 ����")]
        public bool isCollisionPlayer;
        /// <summary>
        /// Ư�� ���� ���� ������ �÷��̾�� ���� ����
        /// </summary>
        [Header("�÷��̾� ���� ����/����")]
        public bool isCanAttack;

        /// <summary>
        /// ���� �ӵ�
        /// </summary>
        [Header ("���� �ӵ�")]
        public float followSpeed;   
        
        [Header("���� �ӵ�")]
        public float rushSpeed;
        /// <summary>
        /// ���� ���� ����
        /// </summary>
        public bool isCanRush;
        [Header("�ǽð� ��������Ʈ ������ų����")]
        public bool isSpriteCheck;


        /// <summary>
        /// ��Ÿ�� üũ�� State Enum
        /// �����ؾ� �Ҽ��� ����
        /// </summary>
        [Header("�� ������ ���� ����")]
        public MonterState attackState;
        /// <summary>
        /// � ������ ��������
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
    ////�¿��� �׽�Ʈ �������
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