using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardAttack : AttackSkill
{
    [SerializeField] GameObject projectile;
    public float ranThresh;
    public override void Attack()
    {
        var x = Random.Range(transform.position.x - ranThresh, transform.position.x + ranThresh);
        var y = Random.Range(transform.position.y - ranThresh, transform.position.y + ranThresh);
        var go = Instantiate(projectile, new Vector2(x, y), Quaternion.identity) as GameObject;
        entity.SetOutPutDamage(damage);
        go.GetComponent<Projectile>().SetUp(entity.outputDamage, entity.transform.localScale.x);
    }

    public override void ResetAttack()
    {

    }
}
