using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEffect : ItemEffect
{
    [SerializeField] GameObject projectile;
    public override void ApplyEffect()
    {
        if (entity && projectile)
        {
            EffectBehaviors.ProjectileSpawn(entity.outPutDamage, projectile, transform.position, entity.transform.localScale.x);
            ResetCD();
        }
    }
}
