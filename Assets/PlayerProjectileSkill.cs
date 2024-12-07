using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnSkill : AttackSkill
{
    [SerializeField] GameObject spawnObj;
    [SerializeField] Transform pos;
    public override void OnAttacking()
    {
        var go = Instantiate(spawnObj, pos.position, Quaternion.identity);
        go.GetComponent<Projectile>()?.SetUp(totalDamage, entity.transform.localScale.x, LayerMask.NameToLayer(Defines.DetectType.DetectEnemy.ToString()));

    }

    public override void ResetAttack()
    {

    }
}
