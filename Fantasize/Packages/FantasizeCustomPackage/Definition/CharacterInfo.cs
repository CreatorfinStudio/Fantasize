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
        /// ��ų ���� ���ð�
        /// </summary>
        [Tooltip("��ų ���� ���ð�")]
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
        [Header("�� HAVE COIN ��")]
        [Tooltip("������ ��ȭ ����")]
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
        [Tooltip("Ư������ D�� �����ð�")]
        public float castingSpeed; //Ư������ D�� �����ð�

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
        [Header("���� �ӵ�")]
        public float followSpeed;

        [Header("���� �ӵ�")]
        public float rushSpeed;
        /// <summary>
        /// ���� ���� ����
        /// </summary>
        public bool isCanRush;

        [Header("�ǽð� ���� ������ų����")]
        public bool isDirectionCheck;

        [Header("�������� �ݶ��̴� (���� �ݶ��̴�) ")]
        public BoxCollider2D attackCollider;

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
        public bool IsDirectionCheck { get { return isDirectionCheck; } set { isDirectionCheck = value; } }
        public BoxCollider2D AttackCollider { get { return attackCollider; } set { attackCollider = value; } }

        public MonterState AtackState { get { return attackState; } set { attackState = value; } }
        public MonsterType MonsterType { get { return monsterType; } set { monsterType = value; } }
        #endregion
    }
}