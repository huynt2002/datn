using System;
using UnityEngine;
[Serializable]
public class PlayerStats : Entity
{
    public static PlayerStats instance;
    public int coin { get; private set; }
    public int gem { get; private set; }
    public int skillId { get; private set; }
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        SetDefault();
    }

    void Start()
    {
        if (GameManager.instance.gameData == null)
        {
            return;
        }
        if (GameManager.instance.gameData.playerStats != null)
        {
            var data = GameManager.instance.gameData.playerStats;
            SetDataStats(data.currentHP, data.maxHP, data.damage, data.speed, data.coin, data.gem, data.criticalChance, data.criticalDamage, data.skillId);
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

    void SetDataStats(float currentHP, float maxHP, float damage, float speed, int coin, int gem, float criticalChance, float criticalDamage, int skillId)
    {
        this.currentHP = currentHP;
        this.maxHP = maxHP;
        this.damage = damage;
        this.speed = speed;
        this.coin = coin;
        this.gem = gem;
        this.criticalChance = criticalChance;
        this.criticalDamage = criticalDamage;
        this.skillId = skillId;
    }

    public override void SetDefault()
    {
        base.SetDefault();
        coin = 0;
        gem = 0;
        skillId = -1;
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

    public void SetSkill(int id)
    {
        skillId = id;
    }

}
