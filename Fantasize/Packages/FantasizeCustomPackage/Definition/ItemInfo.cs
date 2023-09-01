
using UnityEngine;
using UnityEngine.UI;

namespace Definition
{
    [System.Serializable]
    public class Item
    {
        public string name;
        public Sprite icon;
        public int hp;
        public int hungry;

        #region property   
        public string Name { get { return name; } }
        public Sprite Icon { get { return icon; } }
        public int HP { get { return hp; } set { hp = value; } }
        public int Hungry { get { return hungry; } set { hungry = value; } }

        #endregion
    }
    [System.Serializable]

    public class FieldItem : Item
    {
    }
    [System.Serializable]

    public class Trap : Item
    {
    }

    [System.Serializable]
    public class InteractionItem : Item
    {
        public float applicationTime; //아이템 적용시간
        public float moveSpeed;
        public float attackSpeed;

        public float rangedView;
        public float forwardView;

        #region property       

        public float ApplicationTime { get { return applicationTime; } set { applicationTime = value; } }
        public float MoveSpeed { get { return moveSpeed; } set { moveSpeed = value; } }
        public float AttackSpeed { get { return attackSpeed; } set { attackSpeed = value; } }
        public float RangedView { get { return rangedView; } set { rangedView = value; } }
        public float ForwardView { get { return forwardView; } set { forwardView = value; } }
        #endregion
    }

    //public class ActiveItem : InteractionItem
    //{

    //}
    //public class PassiveItem : InteractionItem
    //{

    //}

    /// <summary>
    /// 무기 슬롯 데이터
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [System.Serializable]

    public class WeaponSlot<T>
    {
        public T weapon;
        public T Weapon { get { return weapon; } set { weapon = value; } }
    }

}