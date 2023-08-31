using Control;
using Definition;
using UnityEngine;

namespace Player
{
    public class PlayerService : MonoBehaviour , IPlayerInfo
    {
        public PlayerInfo playerInfo;
        private void Start()
        {
            SetComponent();
            SetInitInfo();
        }

        private void SetComponent()
        {
            this.gameObject.AddComponent<KeyboardMove>();
            this.gameObject.AddComponent<MouseRotation>();
            this.gameObject.AddComponent<ComboAttack>();
            this.gameObject.AddComponent<LongClick>();
        }
        private void SetInitInfo()
        {
            playerInfo.Hp = 100;
            playerInfo.Hungry = 100;

        }

        #region PlayerInfo Data Interface
        public int GetHp() => playerInfo.Hp;
        public int GetHungry() => playerInfo.Hungry;

        public float GetMoveSpeed() => playerInfo.MoveSpeed;
        public float GetAttackPower() => playerInfo.AttackPower;
        public float GetAttackSpeed() => playerInfo.AttackSpeed;
        public float GetRotationSpeed() => playerInfo.RotationSpeed;

        public float GetRangedView() => playerInfo.RangedView;
        public float GetForwardView() => playerInfo.ForwardView;

        public int GetMaxHP() => playerInfo.MaxHP;
        public int GetMaxHungry() => playerInfo.MaxHungry;


        public void SetHp(int hp) => playerInfo.Hp += hp;
        public void SetHungry(int hungry) => playerInfo.Hungry += hungry;

        public void SetMoveSpeed(float moveSpeed) => playerInfo.MoveSpeed += moveSpeed;
        public void SetAttackPower(float attackPower) => playerInfo.AttackPower += attackPower;
        public void SetAttackSpeed(float attackSpeed) => playerInfo.AttackSpeed += attackSpeed;
        public void SetRotationSpeed(float rotationSpeed) => playerInfo.RotationSpeed += rotationSpeed;
        public void SetRangedView(float rangedView) => playerInfo.RangedView += rangedView;
        public void SetForwardView(float forwardView) => playerInfo.ForwardView += forwardView;
        public void SetMaxHungry(int maxHungry) => playerInfo.MaxHungry += maxHungry;
        public void SetMaxHP(int maxHP) => playerInfo.MaxHP += maxHP;
        #endregion
    }
}