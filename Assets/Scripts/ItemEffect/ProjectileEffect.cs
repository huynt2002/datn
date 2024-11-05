using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEffect : ItemEffect
{
    [SerializeField] GameObject projectile;
    ProjectileBehavior.Target target;
    public override void ApplyEffect()
    {
        if (entity.GetComponent<PlayerStats>())
        {
            target = ProjectileBehavior.Target.Enemy;
        }
        else
        {
            target = ProjectileBehavior.Target.Player;
        }
        if (projectile)
        {
            var projectileObject = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
            projectileObject.GetComponent<ProjectileBehavior>().Set(target, entity.outPutDamage);
        }
    }
}
