using UnityEngine;

public class EffectBehaviors
{
    public static void Healing(Entity entity, int healNum, Transform transform)
    {
        entity.HealHP(healNum);
        var uiEffect = SpawnManager.instance.SpawnEffect(SpawnManager.EffectType.PlayerHealingEffect, transform, Helper.GetPos(entity.gameObject));
    }

    public static void ProjectTileSpawn(Entity entity, GameObject projectile, Vector2 pos)
    {
        var xPos = Random.Range(pos.x - 1f, pos.x + 1f);
        var yPos = Random.Range(pos.y - 1f, pos.y + 1f);
        var projectileObject = Object.Instantiate(projectile, new Vector2(xPos, yPos), Quaternion.identity) as GameObject;
        if (entity.GetComponent<PlayerStats>())
        {
            projectileObject.GetComponent<ProjectileBehavior>().Set(ProjectileBehavior.Target.Enemy, entity.outPutDamage);
        }
        else
        {
            projectileObject.GetComponent<ProjectileBehavior>().Set(ProjectileBehavior.Target.Player, entity.outPutDamage);
        }
    }
}