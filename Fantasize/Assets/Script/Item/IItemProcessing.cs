using Definition;
using UnityEngine;

public interface IItemProcessing
{
    ////////////////////Get////////////////////
    public string GetName();
    public Sprite GetImage();
    public int GetPrice();
    public float GetHp();
    public float GetMaxHp();
    public float GetAttackDamage();
    public float GetAttackSpeed();
    public float GetMoveSpeed();
    public float GetSpecialAttackDamage();
    public float GetCastingSpeed();
    public ItemCalculation GetCalculation();

    ////////////////////Set////////////////////
    
    public void SetName(string name);
    public void SetImage(Sprite img);
    public void SetPrice(int price);
    public void SetHp(float hp);
    public void SetMaxHp(float maxhp);
    public void SetAttackDamage(float attackdamage);
    public void SetAttackSpeed(float attackspeed);
    public void SetMoveSpeed(float movespeed);
    public void SetSpecialAttackDamage(float specialattackdamage);
    public void SetCastingSpeed(float castingspeed);
    public void SetCalculation(ItemCalculation calculation);

}
