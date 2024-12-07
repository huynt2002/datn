using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAttack : AttackSkill
{
    Rigidbody2D body;
    public float force;
    void Start()
    {
        body = GetComponentInParent<Rigidbody2D>();
    }

    public override void ResetAttack()
    {
        body.velocity = new Vector2(0, body.velocity.y);
    }

    public override void Attack()
    {
        entity.SetOutPutDamage(damage);
        //body.velocity = new Vector2(force * transform.localScale.x, 0);
        int direction = 1;
        Monster_Behavior monster_Behavior = body.GetComponent<Monster_Behavior>();
        if (monster_Behavior.facingDirection == monster_Behavior.faceRight)
        {
            direction = 1;
        }
        else { direction = -1; }
        body.AddForce(new Vector2(force * direction, 0), ForceMode2D.Impulse);
    }

}
