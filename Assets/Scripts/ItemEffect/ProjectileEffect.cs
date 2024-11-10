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
            EffectBehaviors.ProjectTileSpawn(entity, projectile, transform.position);
            ResetCD();
        }
    }
}
