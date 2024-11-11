using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttack : AttackSkill
{
    Rigidbody2D body;
    public float force = 0;

    void Start()
    {
        body = GetComponentInParent<Rigidbody2D>();
    }

    public override void ResetAttack()
    {

    }

    public override void Attack()
    {
        int direction = 1;
        Monster_Behavior monster_Behavior = body.GetComponent<Monster_Behavior>();
        entity.SetOutPutDamage(damage);
        if (monster_Behavior.facingDirection != monster_Behavior.faceRight)
        {
            direction = -1;
        }
        body.AddForce(new Vector2(force * direction, 0), ForceMode2D.Impulse);
    }
}
