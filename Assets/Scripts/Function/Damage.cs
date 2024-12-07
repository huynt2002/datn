using System.Collections;
using UnityEngine;


public class Damage : MonoBehaviour
{
    Entity entity;
    Projectile projectile;
    protected float damage;
    protected bool isCritical;
    protected Defines.DamageType damageType;

    protected void Start()
    {
        entity = GetComponentInParent<Entity>();
        projectile = GetComponent<Projectile>();
        if (entity)
        {
            damageType = Defines.DamageType.Entity;
            return;
        }
        if (projectile)
        {
            damageType = Defines.DamageType.Projectile;
            return;
        }
    }

    protected void Effect(Vector2 position)
    {
        // Set correct arrow spawn position
        GameObject dust = SpawnManager.instance.SpawnEffect(SpawnManager.EffectType.HitEffect, position);
    }

    protected void UpdateDamage()
    {
        switch (damageType)
        {
            case Defines.DamageType.Entity:
                damage = entity.outputDamage;
                break;
            case Defines.DamageType.Projectile:
                damage = projectile.damage;
                break;
        }
    }

    protected void GetCriticalHit()
    {
        if (!entity)
        {
            return;
        }
        var p = Random.Range(0, 100);
        if (p <= entity.criticalChance)
        {
            damage = damage * entity.criticalDamage / 100;
            isCritical = true;
            return;
        }
        isCritical = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Entity target = other.GetComponentInParent<Entity>();
        UpdateDamage();
        GetCriticalHit();
        if (!target.isAlive) { return; }
        //deal damage
        Effect(Helper.GetPos(target.gameObject));
        if (target.invicible) { return; }
        target.TakeDamage(damage, damageType, isCritical);
        var onHitEffects = GetComponents<OnHitEffect>();
        foreach (var onHitEffect in onHitEffects)
        {
            onHitEffect.OnHit(target);
        }
    }
}
