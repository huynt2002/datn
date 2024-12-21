using System;
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

    public bool isAlive { get; protected set; }
    public bool invicible { get; protected set; }

    public bool getHit { get; protected set; }
    private void Awake()
    {
        SetDefault();
    }
    void Start()
    {

    }

    virtual public void SetDefault()
    {
        if (stats == null)
        {
            Debug.LogError("No stats");
            return;
        }
        damage = stats.damage;
        maxHP = stats.maxHP;
        currentHP = maxHP;
        criticalChance = stats.criticalChance;
        criticalDamage = stats.criticalDamage;
        speed = stats.speed;

        isAlive = true;
        getHit = false;
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
        if (ui) { ui.Show(); }
        currentHP -= damage;
        if (currentHP <= 0)
        {
            currentHP = 0;
            isAlive = false;
            Dead();
        }
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
        amount = (int)amount;
        currentHP += amount;
        if (currentHP > maxHP) currentHP = maxHP;
        DamagePopUpManager.instance?.Create(Helper.GetPos(gameObject, Helper.ObjPosition.Top), amount, false, true);
    }

    public void IncreaseDmg(float dmg)
    {
        damage = damage * (100 + dmg) / 100;
    }

    public void DecreaseDmg(float dmg)
    {
        damage = damage * 100 / (100 + dmg);
    }

    public void ResetGetHit()
    {
        getHit = false;
    }
}
