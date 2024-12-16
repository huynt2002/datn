using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEffect : MultiTimeEffect
{
    [SerializeField] GameObject projectile;
    public override void ApplyEffect()
    {
        if (entity && projectile)
        {
            EffectBehaviors.ProjectileSpawn(entity.damage, projectile, Helper.GetPos(entity.gameObject, Helper.ObjPosition.Center), entity.transform.localScale.x);
            ResetCD();
        }
    }

    public override void DestroyEffect()
    {
        Destroy(gameObject);
    }
}
