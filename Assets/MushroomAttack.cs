using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomAttack : AttackSkill
{
    [SerializeField] GameObject projectile;
    [SerializeField] Transform pos;
    public override void Attack()
    {
        if (projectile != null)
        {
            var go = Instantiate(projectile, pos.position, Quaternion.identity);
            go.GetComponent<Projectile>().SetUp(entity.outPutDamage, entity.transform.localScale.x);
        }
        else
        {
            Debug.LogError("Projectile Null");
        }
    }

    public override void ResetAttack()
    {

    }
}
