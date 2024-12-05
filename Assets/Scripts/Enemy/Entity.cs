using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Entity : MonoBehaviour
{
    [Header("Stats")]
    public EntityStats stats;
    public float currentHP { get; protected set; }
    public float maxHP { get; protected set; }

    public float speed { get; protected set; }

    public float damage { get; protected set; }
    public float criticalChance { get; protected set; }
    public float criticalDamage { get; protected set; }
    public float outputDamage { get; protected set; }

    public bool isAlive { get; protected set; }
    public bool invicible { get; protected set; }

    public bool getHit { get; protected set; }
    void Start()
    {
        SetDefault();
    }

    virtual public void SetDefault()
    {
        if (stats == null) return;
        damage = stats.damage;
        maxHP = stats.maxHP;
        currentHP = maxHP;
        criticalChance = stats.criticalChance;
        criticalDamage = stats.criticalDamage;
        speed = stats.speed;
        isAlive = true;
        getHit = false;
        SetOutPutDamage();
    }

    public void TakeDamage(float _damage, Defines.DamageType _type, bool isCriticalHit = false)
    {
        if (invicible) return;
        getHit = true;
        var damage = (int)_damage;
        DamagePopUpManager.instance?.Create(Helper.GetPos(gameObject, Helper.ObjPosition.Top), damage, isCriticalHit);
        GameObject blood = SpawnManager.instance
            .SpawnParticalEffect(SpawnManager.ParticleType.BloodSmall, Helper.GetPos(gameObject));
        var ui = GetComponent<Monster_Behavior>()?.GetComponentInChildren<DisplayHP>();
        if (ui != null) ui.show = true;
        currentHP -= damage;
        if (currentHP <= 0)
        {
            currentHP = 0;
            isAlive = false;
            Dead();
        }
    }

    public void DealDamage(Entity entity)
    {
        entity?.TakeDamage(outputDamage, Defines.DamageType.Entity);
    }

    protected virtual void Dead()
    {
        //Dead for monster
        DropWhenDie();
        SpawnManager.instance.SpawnParticalEffect(SpawnManager.ParticleType.BloodLarge,
            Helper.GetPos(gameObject));
        SetInvincible(true);
        SoundManager.instance.PlayDeathSound(gameObject.transform);
        Invoke(nameof(Destroy), 5f);
    }

    void Destroy()
    {
        Destroy(gameObject);
    }

    public void SetInvincible(bool set)
    {
        invicible = set;
    }

    void DropWhenDie()
    {
        SpawnManager.instance.DropProps(transform);
    }

    public void SetOutPutDamage(float dame = 0)
    {
        outputDamage = damage + dame / damage;
    }

    public void SetHp(float hp)
    {
        maxHP = hp;
    }

    public void IncreaseHP(float amount)
    {
        maxHP += amount;
        currentHP += amount;
    }

    public void DecreaseHP(float amount)
    {
        maxHP -= amount;
        var remain = currentHP - amount;
        if (remain > 0)
        {
            currentHP = remain;
        }
        else
        {
            currentHP = 1;
        }
    }

    public void IncreaseSpeed(float value)
    {
        speed += value;
    }

    public void IncreaseCriticalChance(float amount)
    {
        criticalChance += amount;
    }

    public void IncreaseCriticalDamage(float amount)
    {
        criticalDamage += amount;
    }

    public void HealHP(float amount)
    {
        currentHP += amount;
        if (currentHP > maxHP) currentHP = maxHP;
        DamagePopUpManager.instance?.Create(transform.position, amount, false, true);
    }

    public void IncreaseDmg(float dmg)
    {
        damage = damage * (100 + dmg) / 100;
        SetOutPutDamage();
    }

    public void DecreaseDmg(float dmg)
    {
        damage = damage * 100 / (100 + dmg);
        SetOutPutDamage();
    }

    public void ResetGetHit()
    {
        getHit = false;
    }
}
