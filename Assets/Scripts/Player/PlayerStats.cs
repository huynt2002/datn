using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class PlayerStats : Entity
{
    public static PlayerStats instance;
    public float criticalChance { get; private set; }
    public float criticalDamage { get; private set; }
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
            SetDataStats(data.currentHP, data.maxHP, data.damage, data.speed, data.coin, data.gem, data.criticalChance, data.criticalDamage);
            SetOutPutDamage();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //just for demonstrate
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            damage += 10;
            return;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            coin += 1000;
            return;
        }
    }

    void SetDataStats(float currentHP, float maxHP, float damage, float speed, int coin, int gem, float criticalChance, float criticalDamage)
    {
        this.currentHP = currentHP;
        this.maxHP = maxHP;
        this.damage = damage;
        this.speed = speed;
        this.coin = coin;
        this.gem = gem;
        this.criticalChance = criticalChance;
        this.criticalDamage = criticalDamage;
    }

    public override void SetDefault()
    {
        maxHP = 100;
        currentHP = maxHP;
        speed = 5;
        isAlive = true;
        damage = 15;
        SetOutPutDamage();
        coin = 0;
        gem = 0;
    }

    protected override void Dead()
    {
        gameObject.SetActive(false);
        GameManager.instance.isLose = true;
    }

    public void AddGem(int g)
    {
        gem += g;
    }

    public void AddCoin(int c)
    {
        coin += c;
    }

    public void IncreaseCriticalChance(float amount)
    {
        criticalChance += amount;
    }

    public void IncreaseCriticalDamage(float amount)
    {
        criticalDamage += amount;
    }
}
