using System;

namespace Definition
{
    public class CharacterProperty
    {        
        protected float moveSpeed;
        protected float attackPower;
        protected float attackSpeed;

        protected float rotationSpeed; //테스트용. 기획서에는 없음.

        #region Property        
        public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }
        public float AttackPower { get { return attackPower; } set { attackPower = value; } }
        public float AttackSpeed { get { return attackSpeed; } set { attackSpeed = value; } }
        public float RotationSpeed { get { return rotationSpeed; } set { rotationSpeed = value; } }

        #endregion

        public CharacterProperty() { }
        public CharacterProperty(float moveSpeed, float attackPower, float attackSpeed)
        {
            this.moveSpeed = moveSpeed;       
            this.moveSpeed = moveSpeed;
            this.attackPower = attackPower;
            this.attackSpeed = attackSpeed;
        }

        //테스트용
        public CharacterProperty(float moveSpeed, float rotationSpeed)
        {
            this.moveSpeed = moveSpeed;
            this.rotationSpeed = rotationSpeed;
        }
    }

    public class PlayerInfo : CharacterProperty
    {
        private float hungry;
        private float hp;
        private float view;

        #region Property
        public float Hungry { get { return hungry; } set { hungry = value; } }
        public float Hp { get { return hp; } set { hp = value; } }
        public float View { get { return view; } set { view = value; } }
        #endregion

        public PlayerInfo(float hungry, float hp, float view) 
        {
            this.hungry = hungry;
            this.hp = hp;
            this.view = view;            
        }
    }

    public class MonsterInfo : CharacterProperty
    {        

    }

    //승원이 테스트 윈도우용
    public struct TestWindowProperty
    {
        private float moveSpeed;
        private float rotationSpeed;
        private int maxComboAttacks;

        public float MoveSpeed
        {
            get { return moveSpeed; }
            set
            {
                moveSpeed = value;
                Test_ChangedMoveSpeed?.Invoke(value);
            }
        }
        public float RotationSpeed
        {
            get { return rotationSpeed; }
            set
            {
                rotationSpeed = value;
                Test_ChangedRotationSpeed?.Invoke(value);
            }
        }
        public int MaxComboAttacks
        {
            get { return maxComboAttacks; }
            set
            {
                maxComboAttacks = value;
                Test_ChangeMaxComboAttacks?.Invoke(value);
            }
        }

        public event Action<float> Test_ChangedMoveSpeed;
        public event Action<float> Test_ChangedRotationSpeed;
        public event Action<int> Test_ChangeMaxComboAttacks;
    }
}