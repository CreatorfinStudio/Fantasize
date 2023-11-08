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
        public float attackSpeed;

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
        [Space(10)]
        public int hungry;
        public float rangedView;
        public float forwardView;

        /// <summary>
        /// �ӽ� �߰�.
        /// </summary>
        public float jumpForce;
                
        public float runSpeed;
        public float runJumpForce;

        public int maxHungry;
        public int maxHP;

        /// <summary>
        /// Move FSM
        /// </summary>
        public PlayerState moveFSM;

        #region Property
        public int Hungry { get { return hungry; } set { hungry = value; } }
        public float RangedView { get { return rangedView; } set { rangedView = value; } }
        public float ForwardView { get { return forwardView; } set { forwardView = value; } }
        public float JumpForce { get { return jumpForce; }set { jumpForce = value; } }
        public float RunSpeed {get { return runSpeed; } set { runSpeed = value; } }
        public float RunJumpForce { get { return runJumpForce; } set { runJumpForce = value; } }
        public int MaxHungry { get { return maxHungry; } set { maxHungry = value; } }
        public int MaxHP { get { return maxHP; } set { maxHP = value; } }
        public PlayerState MOVEFSM { get { return moveFSM; } set { moveFSM = value; } }
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
        /// �� ���� ���� ������ �÷��̾ ���󰡱� ������
        /// </summary>
        [Header ("������ �����ϴ� �Ÿ�")]
        public float followRange;
        /// <summary>
        /// ���� �ӵ�
        /// </summary>
        [Header ("���� �ӵ�")]
        public float followSpeed;
        /// <summary>
        /// �� ���� ���� ������ �÷��̾�� ���� ����
        /// </summary>
        [Header("������ �����ϴ� �Ÿ�")]
        public float atackRange;
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
        public float FollowRange { get { return followRange; } set { followRange = value; } }
        public float FollowSpeed { get { return followSpeed; } set { followSpeed = value; } }
        public float AtackRange { get { return atackRange; } set { atackRange = value; } }
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