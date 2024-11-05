using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Stats")]
    public EntityStats stats;
    public float CurrentHP { get; protected set; }
    public float Damage { get; protected set; }
    public float MaxHP { get; protected set; }
    public float DEF { get; protected set; }
    public float speed { get; protected set; }
    public bool IsAlive { get; protected set; }
    public bool invicible { get; protected set; }
    public float outPutDamage { get; protected set; }
    public bool getHit;
    void Start()
    {
        SetDefault();
    }

    virtual public void SetDefault()
    {
        if (stats == null) return;
        Damage = stats.Damage;
        MaxHP = stats.MaxHP;
        DEF = stats.DEF;
        CurrentHP = MaxHP;
        IsAlive = true;
        getHit = false;
        speed = stats.Speed;
        SetOutPutDamage();
    }

    public void TakeDamage(float _damage, Defines.DamageType _type, bool isCriticalHit = false)
    {
        if (invicible) return;
        if (_type != Defines.DamageType.Trap)
        {
            _damage = _damage - DEF;
        }
        float damage = _damage > 0 ? (int)_damage : 0;
        DamagePopUpManager.instance?.Create(gameObject.transform.position, damage, isCriticalHit);
        GameObject blood = SpawnManager.instance
            .SpawnParticalEffect(SpawnManager.ParticleType.BloodSmall, gameObject.transform.position);
        var ui = GetComponent<Monster_Behavior>()?.GetComponentInChildren<DisplayHP>();
        if (ui != null) ui.show = true;
        CurrentHP -= damage;
        if (CurrentHP <= 0)
        {
            CurrentHP = 0;
            IsAlive = false;
            Dead();
        }
    }

    public void DealDamage(Entity entity)
    {
        entity?.TakeDamage(outPutDamage, Defines.DamageType.Entity);
    }

    protected virtual void Dead()
    {
        //Dead for monster
        DropWhenDie();
        var collider2Ds = gameObject.GetComponentsInChildren<Collider2D>();
        foreach (var collider in collider2Ds)
        {
            if (collider.gameObject.tag == "Enemy")
            {
                SpawnManager.instance.SpawnParticalEffect(SpawnManager.ParticleType.BloodLarge, collider.transform.position);
            }
        }
        SoundManager.instance.PlayDeathSound(gameObject.transform);
        Invoke(nameof(Destroy), 5f);
    }

    void Destroy()
    {
        Destroy(gameObject);
    }

    public void Invincible(bool set)
    {
        invicible = set;
    }

    void DropWhenDie()
    {
        SpawnManager.instance.DropProps(transform);
    }

    public void SetOutPutDamage(float dame = 0)
    {
        outPutDamage = Damage + dame / Damage;
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
}
