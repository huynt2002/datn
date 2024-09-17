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
    void Start()
    {
        StatsApplied();
    }
    public void TakeDamage(float _damage, Defines.DamageType _type)
    {
        if (invicible) return;
        float damage = 0;
        if (_type != Defines.DamageType.Trap)
        {
            damage = _damage - DEF;
        }
        damage = (int)_damage;
        DamagePopUpManager.instance?.Create(gameObject.transform.position, damage);
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

    void StatsApplied()
    {
        if (stats == null) return;
        Damage = stats.Damage;
        MaxHP = stats.MaxHP;
        DEF = stats.DEF;
        CurrentHP = MaxHP;
        IsAlive = true;
        speed = stats.Speed;
        SetOutPutDamage();
    }

    public void DealDamage(Entity entity)
    {
        entity?.TakeDamage(outPutDamage, Defines.DamageType.Entity);
    }

    protected virtual void Dead()
    {
        if (!IsAlive)
        {
            DropWhenDie();
            SoundManager.instance.PlayDeathSound(gameObject.transform);
            Destroy(gameObject);
        }
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
}
