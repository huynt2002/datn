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
        var p = Random.Range(0, 100);
        if (p <= 10)
        {
            damage = damage * 2;
            isCritical = true;
        }
        isCritical = false;
    }
}
