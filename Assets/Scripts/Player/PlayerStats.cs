using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : Entity
{
    public static PlayerStats instance;
    public int coin { get; private set; }
    public int gem { get; private set; }
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        coin = 10000;
        gem = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetDefault()
    {
        MaxHP = 100;
        DEF = 10;
        CurrentHP = MaxHP;
        speed = 5;
        IsAlive = true;
        Damage = 10;
        SetOutPutDamage();
    }

    protected override void Dead()
    {
        if (!IsAlive)
        {
            gameObject.SetActive(false);
            GameManager.instance.isLose = true;
        }
    }

    public void SetHp(float hp)
    {
        MaxHP = hp;
    }

    public void IncreaseHP(float amount)
    {
        MaxHP += amount;
        CurrentHP += amount;
    }

    public void HealHP(float amount)
    {
        CurrentHP += amount;
        if (CurrentHP > MaxHP) CurrentHP = MaxHP;
        DamagePopUpManager.instance?.Create(transform.position, amount, false, true);
    }

    public void SetDEF(float def)
    {
        DEF = def;
    }

    public void SetDMG(float dmg)
    {
        Damage = dmg;
        SetOutPutDamage();
    }

    public void SetGem(int g)
    {
        gem = g;
    }

    public void SetCoin(int c)
    {
        coin = c;
    }


}
