using System;

namespace Definition
{    
    public class CharacterProperty
    {
        public int hp;

        public float moveSpeed;
        public float attackPower;
        public float attackSpeed;

        // 0905 기획에서 마우스 회전 관련 삭제됨
        // public float rotationSpeed; 

        /// <summary>
        /// 스킬 재사용 대기시간
        /// </summary>
        public float reloadTime; 

        #region Property        
        public int Hp { get { return hp; } set { hp = value; } }
        public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }
        public float AttackPower { get { return attackPower; } set { attackPower = value; } }
        public float AttackSpeed { get { return attackSpeed; } set { attackSpeed = value; } }
        // 0905 기획에서 마우스 회전 관련 삭제됨
       // public float RotationSpeed { get { return rotationSpeed; } set { rotationSpeed = value; } }

        #endregion

        public CharacterProperty() { }
        public CharacterProperty(int hp, float moveSpeed, float attackPower, float attackSpeed)
        {
            this.hp = hp;
            this.moveSpeed = moveSpeed;
            this.moveSpeed = moveSpeed;
            this.attackPower = attackPower;
            this.attackSpeed = attackSpeed;
        }
    }

    [System.Serializable]
    public class PlayerInfo : CharacterProperty
    {
        public int hungry;
        public float rangedView;
        public float forwardView;

        /// <summary>
        /// 임시 추가.
        /// </summary>
        public float jumpForce;
                
        public float runSpeed;
        public float runJumpForce;

        public int maxHungy;
        public int maxHP;

        #region Property
        public int Hungry { get { return hungry; } set { hungry = value; } }
        public float RangedView { get { return rangedView; } set { rangedView = value; } }
        public float ForwardView { get { return forwardView; } set { forwardView = value; } }
        public float JumpForce { get { return jumpForce; }set { jumpForce = value; } }
        public float RunSpeed {get { return runSpeed; } set { runSpeed = value; } }
        public float RunJumpForce { get { return runJumpForce; } set { runJumpForce = value; } }
        public int MaxHungry { get { return maxHungy; } set { maxHungy = value; } }
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
        /// 이 범위 내로 들어오면 플레이어를 따라가기 시작함
        /// </summary>
        public float followRange;
        /// <summary>
        /// 이 범위 내로 들어오면 플레이어에게 공격 시전
        /// </summary>
        public float atackRange;
        /// <summary>
        /// 쿨타임 체크용 State Enum
        /// 수정해야 할수도 있음
        /// </summary>
        public MonterState attackState;
        /// <summary>
        /// 어떤 종류의 몬스터인지
        /// </summary>
        public MonsterType monsterType;

        #region Property
        public float FollowRange { get { return followRange; } set {  followRange = value; } }
        public float AtackRange { get { return atackRange; } set { atackRange = value; } }
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