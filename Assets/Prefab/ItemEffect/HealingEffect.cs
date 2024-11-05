using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingEffect : ItemEffect
{
    public int healNum = 5;
    public float healCD = 5f;
    float count = 0;
    public override void ApplyEffect()
    {
        var entity = gameObject.GetComponentInParent<Entity>();
        if (entity)
        {
            entity.HealHP(healNum);
            var uiEffect = SpawnManager.instance.SpawnEffect(SpawnManager.EffectType.PlayerHealingEffect, transform);
        }
    }

    public override bool Check()
    {
        if (count < healCD)
        {
            count += Time.deltaTime;
            return false;
        }
        count = 0;
        return true;
    }
}
