using UnityEngine;

public class EffectBehaviors
{
    public static void Healing(Entity entity, int healNum, Transform transform)
    {
        entity.HealHP(healNum);
        var uiEffect = SpawnManager.instance.SpawnEffect(SpawnManager.EffectType.PlayerHealingEffect, transform, Helper.GetPos(entity.gameObject));
    }

    public static void ProjectileSpawn(float damage, GameObject projectile, Vector2 pos, float direction)
    {
        var xPos = Random.Range(pos.x - 1f, pos.x + 1f);
        var yPos = Random.Range(pos.y - 1f, pos.y + 1f);
        var projectileObject = Object.Instantiate(projectile, new Vector2(xPos, yPos), Quaternion.identity) as GameObject;
        projectileObject.GetComponent<Projectile>().SetUp(damage, direction);
    }
}