using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardTraitEffect : ItemTraitEffect
{
    [SerializeField] GameObject projectile;
    public override void FirstLevel()
    {

    }

    public override void SecondLevel()
    {
        EffectBehaviors.ProjectTileSpawn(entity, projectile, transform.position);
        ResetCD();
    }

    public override void ThirdLevel()
    {

    }
}
