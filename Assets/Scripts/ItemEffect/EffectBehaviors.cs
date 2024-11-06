using UnityEngine;

public class EffectBehaviors
{
    public static void Healing(Entity entity, int healNum, Transform transform)
    {
        entity.HealHP(healNum);
        var uiEffect = SpawnManager.instance.SpawnEffect(SpawnManager.EffectType.PlayerHealingEffect, transform);
    }
}