using UnityEngine;

namespace Definition
{
    public class CharacterProperty
    {
        public float hp;
        public float maxHP;

        public float moveSpeed;
        public float attackPower;
        public float specialAttackPower;
        public float attackSpeed;
        public float jumpForce;

        /// <summary>
        /// 스킬 재사용 대기시간
        /// </summary>
        [Tooltip("스킬 재사용 대기시간")]
        public float reloadTime;

        #region Property        
        public float Hp { get { return hp; } set { hp = value; } }
        public float MaxHP { get { return maxHP; } set { maxHP = value; } }

        public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }
        public float AttackPower { get { return attackPower; } set { attackPower = value; } }
        public float SpecialAttackPower { get { return specialAttackPower; } set { specialAttackPower = value; } }
        public float AttackSpeed { get { return attackSpeed; } set { attackSpeed = value; } }
        public float JumpForce { get { return jumpForce; } set { jumpForce = value; } }

        #endregion

        public CharacterProperty() { }
        public CharacterProperty(int hp, float moveSpeed, float attackPower, float attackSpeed)
        {
            this.hp = hp;
            this.moveSpeed = moveSpeed;
            this.attackPower = attackPower;
            this.attackSpeed = attackSpeed;
        }
    }

    [System.Serializable]
    public class PlayerInfo : CharacterProperty
    {
        [Header("☆ HAVE COIN ☆")]
        [Tooltip("보유한 재화 개수")]
        public int haveCoin;

        [Header("== Behavior ==")]
        [Space(7)]
        public bool isDashing;
        public bool isJumping;

        public bool isCanJump;
        public float dashDirection;
        [Header("===========")]
        [Space(7)]
        public AttackType attackType;
        [Space(5)]
        [Tooltip("특수공격 D꾹 시전시간")]
        public float castingSpeed; //특수공격 D꾹 시전시간

        [Space(5)]
        public float rangedView;
        public float forwardView;

        [Space(5)]
        public float dashSpeed;
        public float runJumpForce;

        #region Property
        public int HaveCoin { get { return haveCoin; } set { haveCoin = value; } }
        public bool IsDashing { get { return isDashing; } set { isDashing = value; } }
        public bool IsJumping { get { return isJumping; } set { isJumping = value; } }
        public bool IsCanJump { get { return isCanJump; } set { isCanJump = value; } }

        public float DashDirection { get { return dashDirection; } set { dashDirection = value; } }
        public AttackType AttackType { get { return attackType; } set { attackType = value; } }
        public float CastingSpeed { get { return castingSpeed; } set { castingSpeed = value; } }

        public float RangedView { get { return rangedView; } set { rangedView = value; } }
        public float ForwardView { get { return forwardView; } set { forwardView = value; } }
        public float DashSpeed { get { return dashSpeed; } set { dashSpeed = value; } }

        public float RunJumpForce { get { return runJumpForce; } set { runJumpForce = value; } }


        #endregion

        public PlayerInfo(float rangedView, float forwardView, int maxHP)
        {
            this.rangedView = rangedView;
            this.ForwardView = forwardView;
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
        [Header("추적 속도")]
        public float followSpeed;

        [Header("돌진 속도")]
        public float rushSpeed;
        /// <summary>
        /// 돌진 가능 여부
        /// </summary>
        public bool isCanRush;

        [Header("실시간 방향 반전시킬건지")]
        public bool isDirectionCheck;

        [Header("공격판정 콜라이더 (무기 콜라이더) ")]
        public BoxCollider2D attackCollider;

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
        public bool IsDirectionCheck { get { return isDirectionCheck; } set { isDirectionCheck = value; } }
        public BoxCollider2D AttackCollider { get { return attackCollider; } set { attackCollider = value; } }

        public MonterState AtackState { get { return attackState; } set { attackState = value; } }
        public MonsterType MonsterType { get { return monsterType; } set { monsterType = value; } }
        #endregion
    }
}