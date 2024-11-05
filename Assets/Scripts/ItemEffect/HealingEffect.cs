using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingEffect : ItemEffect
{
    public int healNum = 5;
    public override void ApplyEffect()
    {
        if (entity)
        {
            entity.HealHP(healNum);
            var uiEffect = SpawnManager.instance.SpawnEffect(SpawnManager.EffectType.PlayerHealingEffect, transform);
        }
    }
}
