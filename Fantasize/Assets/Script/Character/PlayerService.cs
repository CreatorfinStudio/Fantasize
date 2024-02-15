using BehaviorDesigner.Runtime;
using Definition;
using Manager;
using System;
using UnityEngine;

namespace Player
{
    public class PlayerService : MonoBehaviour, IPlayerInfo
    {
        private static PlayerService instance;

        private float h;
        private Rigidbody2D rb;

        public PlayerInfo playerInfo;
        //Ư������ ������
        public float dPressTime = 0f;

        [Header("��� ���� �ð�")]
        [SerializeField]
        private float playDashTime = 0.8f;
        //��� ����� ���ɿ��� ������
        private BehaviorTree behaviorTree;
        private bool isDashSet = false;

        public static bool canUseRunAnim = false;

        private void Start()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (instance != this)
                Destroy(gameObject);

            dPressTime = 0f;
            behaviorTree = GetComponent<BehaviorTree>();
            rb = GetComponent<Rigidbody2D>();

            SetHp(GetMaxHP());

            GameManager.areaResetEvent += () => {
                if (this != null)
                {
                    this.transform.position = new Vector2(-2.47f, 1.5f);
                    this.transform.localScale = new Vector3(-3, 3, 3);

                    GetComponent<BehaviorTree>().enabled = false;
                    GetComponent<BehaviorTree>().enabled = true;

                    SetHp(GetMaxHP());
                }
            };
        }

        private void Update()
        {
            CheckFlipX();
            SetCurrAttackType();
            SetCanBeDash();
        }

        /// <summary>
        /// �ǽð� ��������Ʈ ����üũ
        /// </summary>
        private void CheckFlipX()
        {
            if (!GetIsDashing())
            {
                h = Input.GetAxis("Horizontal");
                Vector3 currentScale = transform.localScale;

                if (h < 0)
                {
                    if (currentScale.x < 0)
                    {
                        currentScale.x *= -1;
                        transform.localScale = currentScale;
                    }
                }
                else if (h > 0)
                {
                    if (currentScale.x > 0)
                    {
                        currentScale.x *= -1;
                        transform.localScale = currentScale;
                    }
                }
            }
        }
        private void SetCurrAttackType()
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                dPressTime = Time.time;
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                float elapsedTime = Time.time - dPressTime;
                if (elapsedTime <= GetCastingSpeed())
                {
                    if (GetIsJumping())
                        SetAttackType(AttackType.AirAttack);
                    else
                        SetAttackType(AttackType.Attack);
                }
                else
                {
                    SetAttackType(AttackType.SpecialAttack);
                }
            }
        }

        /// <summary>
        /// ��� ��� ���� (��� �� ��ý��� X , ��� ���� ���� �� ��ó��)
        /// </summary>
        private void SetCanBeDash()
        {
            if (GetIsDashing())
            {
                var startTime = behaviorTree.GetVariable("dashStartTime");
                if (!isDashSet)
                {
                    startTime?.SetValue(Time.time);
                    isDashSet = true;
                }
                if (Time.time - (float)startTime?.GetValue() >= playDashTime || Input.GetKeyDown(KeyCode.Space))
                {
                    rb.velocity = Vector2.zero;
                    SetDashDirection(0);
                    SetIsDashing(false);
                }
                rb.velocity = new Vector2(10f * DefinitionManager.Instance.iplayerInfo.GetDashDirection(), rb.velocity.y);
            }
            else
                isDashSet = false;
        }

        #region PlayerInfo Data Interface

        /////////////////////// GET ///////////////////////     
        public float GetHp() => playerInfo.Hp;
        public float GetMaxHP() => playerInfo.MaxHP;

        public int GetHaveCoin() => playerInfo.HaveCoin;

        public bool GetIsDashing() => playerInfo.IsDashing;
        public bool GetIsJumping() => playerInfo.IsJumping;
        public bool GetIsCanJump() => playerInfo.IsCanJump;
        public float GetDashDirection() => playerInfo.DashDirection;
        public AttackType GetAttackType() => playerInfo.AttackType;
        public float GetCastingSpeed() => playerInfo.CastingSpeed;

        public float GetAttackPower() => playerInfo.AttackPower;
        public float GetSpecialAttackPower() => playerInfo.SpecialAttackPower;
        public float GetAttackSpeed() => playerInfo.AttackSpeed;
        public float GetMoveSpeed() => playerInfo.MoveSpeed;
        public float GetRunJumpForce() => playerInfo.RunJumpForce;
        public float GetDashSpeed() => playerInfo.DashSpeed;

        public float GetRangedView() => playerInfo.RangedView;
        public float GetForwardView() => playerInfo.ForwardView;

        /////////////////////// SET ///////////////////////     
        public void SetHp(float hp)
        {
            playerInfo.Hp += hp;
            if (playerInfo.Hp > playerInfo.MaxHP)
                playerInfo.Hp = playerInfo.MaxHP;
        }

        public void SetHaveCoin(int haveCoin) => playerInfo.HaveCoin += haveCoin;

        public void SetIsDashing(bool isDashing) => playerInfo.IsDashing = isDashing;
        public void SetIsJumping(bool isJumping) => playerInfo.IsJumping = isJumping;
        public void SetIsCanJump(bool isCanJump) => playerInfo.IsCanJump = isCanJump;
        public void SetDashDirection(float dashDirection) => playerInfo.DashDirection = dashDirection;
        public void SetAttackType(AttackType attackType) => playerInfo.AttackType = attackType;

        public void SetAttackPower(float attackPower) => playerInfo.AttackPower += attackPower;
        public void SetSpecialAttackPower(float specialAttackPower) => playerInfo.SpecialAttackPower += specialAttackPower;
        public void SetAttackSpeed(float attackSpeed) => playerInfo.AttackSpeed += attackSpeed;
        public void SetCastingSpeed(float castingSpeed) => playerInfo.CastingSpeed += castingSpeed;

        public void SetMoveSpeed(float moveSpeed) => playerInfo.MoveSpeed += moveSpeed;
        public void SetRunJumpForce(float jumpForce) => playerInfo.JumpForce += jumpForce;
        public void SetRangedView(float rangedView) => playerInfo.RangedView += rangedView;
        public void SetForwardView(float forwardView) => playerInfo.ForwardView += forwardView;
        public void SetMaxHP(float maxHP) => playerInfo.MaxHP += maxHP;

        /////////////////// ��/�����꿡 ���� �ٸ� ó�� ///////////////////

        /// <summary>
        /// �տ���
        /// </summary>
        /// <param name="itemInfo"></param>
        public void SetAddItemStatsToPlayer(ItemInfo itemInfo, bool isShop = false)
        {
            //�� ������ ��� �ݾ� ����
            if (isShop)
            {
                int haveCoin = DefinitionManager.Instance.iplayerInfo.GetHaveCoin();

                // ������ ���ݺ��� ������ ���� ������ ���� �Ұ�
                if (haveCoin < itemInfo.Price)
                {
                    Debug.LogError("�� ����.");
                    return;
                }

                // ������ ������ ������ ���� ����
                DefinitionManager.Instance.iplayerInfo.SetHaveCoin(haveCoin - itemInfo.Price);
                GameManager.isItemSelect.Item2 = ItemSource.ShopItem;
            }
            else
                GameManager.isItemSelect.Item2 = ItemSource.DropItem;


            playerInfo.Hp += itemInfo.Hp;
            playerInfo.MaxHP += itemInfo.MaxHp;
            playerInfo.AttackPower += itemInfo.AttackDamage;
            playerInfo.AttackSpeed += itemInfo.AttackSpeed;
            playerInfo.MoveSpeed += itemInfo.MoveSpeed;
            playerInfo.specialAttackPower += itemInfo.SpecialAttackDamage;
            playerInfo.castingSpeed += itemInfo.CastingSpeed;

            GameManager.isItemSelect.Item1 = true;
        }
        /// <summary>
        /// ������ - ���߿� �ٸ������� �߰� ����� ������� �߰��ؾ���
        /// </summary>
        /// <param name="itemInfo"></param>
        public void SetMultiplicationItemStatsToPlayer(ItemInfo itemInfo, bool isShop = false)
        {
            //�� ������ ��� �ݾ� ����
            if (isShop)
            {
                int haveCoin = DefinitionManager.Instance.iplayerInfo.GetHaveCoin();

                if (haveCoin < itemInfo.Price)
                {
                    Debug.LogError("�� ����.");
                    return;
                }

                // ������ ������ ������ ���� ����
                DefinitionManager.Instance.iplayerInfo.SetHaveCoin(haveCoin - itemInfo.Price);
                GameManager.isItemSelect.Item2 = ItemSource.ShopItem;
            }
            else
                GameManager.isItemSelect.Item2 = ItemSource.DropItem;

            //���߿� �����ϼ�
            //0���ϸ� ��� x
            playerInfo.Hp += itemInfo.Hp > 0 ? playerInfo.Hp * itemInfo.Hp : 0;
            playerInfo.MaxHP += itemInfo.MaxHp > 0 ? playerInfo.MaxHP * itemInfo.MaxHp : 0;
            playerInfo.AttackPower += itemInfo.AttackDamage > 0 ? playerInfo.AttackPower * itemInfo.AttackDamage : 0;
            playerInfo.AttackSpeed += itemInfo.AttackSpeed > 0 ? playerInfo.AttackSpeed * itemInfo.AttackSpeed : 0;
            playerInfo.MoveSpeed += itemInfo.MoveSpeed > 0 ? playerInfo.MoveSpeed * itemInfo.MoveSpeed : 0;
            playerInfo.specialAttackPower += itemInfo.SpecialAttackDamage > 0 ? playerInfo.specialAttackPower * itemInfo.SpecialAttackDamage : 0;
            playerInfo.castingSpeed += itemInfo.CastingSpeed > 0 ? playerInfo.castingSpeed * itemInfo.CastingSpeed : 0;


            //playerInfo.Hp *= itemInfo.Hp;
            //playerInfo.MaxHP *= itemInfo.MaxHp;
            //playerInfo.AttackPower *= itemInfo.AttackDamage;
            //playerInfo.AttackSpeed *= itemInfo.AttackSpeed;
            //playerInfo.MoveSpeed *= itemInfo.MoveSpeed;
            //playerInfo.specialAttackPower *= itemInfo.SpecialAttackDamage;
            //playerInfo.castingSpeed *= itemInfo.CastingSpeed;

            GameManager.isItemSelect.Item1 = true;
        }

        /// <summary>
        /// �ӽ� �߰�
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public float GetJumpForce() => playerInfo.JumpForce;

        public void SetJumpForce(float jumpForce)
        {
            playerInfo.JumpForce = jumpForce;
        }
        public void SetDashSpeed(float dashSpeed)
        {
            playerInfo.DashSpeed = dashSpeed;
        }

        #endregion
    }
}