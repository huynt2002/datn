using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class PlayerStats : Entity
{
    public static PlayerStats instance;
    public int coin { get; private set; }
    public int gem { get; private set; }
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        SetDefault();
    }

    void Start()
    {
        if (GameManager.instance.gameData.playerStats != null)
        {
            var data = GameManager.instance.gameData.playerStats;
            SetDataStats(data.CurrentHP, data.MaxHP, data.Damage, data.speed, data.coin, data.gem);
            SetOutPutDamage();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //just for demonstrate
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Damage += 10;
            return;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            coin += 1000;
            return;
        }
    }

    void SetDataStats(float CurrentHP, float MaxHP, float Damage, float speed, int coin, int gem)
    {
        this.CurrentHP = CurrentHP;
        this.MaxHP = MaxHP;
        this.Damage = Damage;
        this.speed = speed;
        this.coin = coin;
        this.gem = gem;
    }

    public override void SetDefault()
    {
        MaxHP = 100;
        CurrentHP = MaxHP;
        speed = 5;
        IsAlive = true;
        Damage = 15;
        SetOutPutDamage();
        coin = 0;
        gem = 0;
    }

    protected override void Dead()
    {
        gameObject.SetActive(false);
        GameManager.instance.isLose = true;
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
