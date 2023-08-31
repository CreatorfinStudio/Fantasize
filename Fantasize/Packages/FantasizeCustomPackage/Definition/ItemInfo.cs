

namespace Definition
{
    [System.Serializable]
    public class Item
    {
        public float applicationTime; //아이템 적용시간

        public int hp;
        public int hungry;
        public float moveSpeed;
        public float attackSpeed;

        public float rangedView;
        public float forwardView;

        public float ApplicationTime { get { return applicationTime; } set { applicationTime = value; } }

        #region property
        public int HP { get { return hp; } set { hp = value; } }
        public int Hungry { get { return hungry; } set { hungry = value; } }
        public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }
        public float AttackSpeed { get { return attackSpeed; } set { attackSpeed = value; } }
        public float RangedView { get { return rangedView; } set { rangedView = value; } }
        public float ForwardView { get { return forwardView; } set { forwardView = value; } }
        #endregion
    }

    public class FieldItem : Item
    {

    }

    public class InteractionItem : Item
    {

    }
    public class ActiveItem : InteractionItem
    {

    }
    public class PassiveItem : InteractionItem
    {

    }
}