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
        SetDefault();
        coin = 10000;
        gem = 0;

    }

    // Update is called once per frame
    void Update()
    {
        //just for demonstrate
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Damage += 10;
        }
        if (getHit)
        {
            Invoke(nameof(ResetGetHit), 1f);
        }
    }

    void ResetGetHit()
    {
        getHit = false;
    }

    public override void SetDefault()
    {
        MaxHP = 100;
        CurrentHP = MaxHP;
        speed = 5;
        IsAlive = true;
        Damage = 15;
        SetOutPutDamage();
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
