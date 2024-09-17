using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnemy : Damage
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    void Hit(Monster_Behavior monster_Behavior)
    {
        if (monster_Behavior)
        {
            if (monster_Behavior.canGetHit)
                monster_Behavior.getHit = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Entity e = other.transform.parent.gameObject.GetComponent<Entity>();
        //deal damage
        Effect(e.gameObject.transform.position);
        if (e.invicible) return;
        Hit(e.GetComponent<Monster_Behavior>());
        e.TakeDamage(entity.outPutDamage, Defines.DamageType.Entity);
        KnockBack(e.GetComponent<Rigidbody2D>());
        Debug.Log("Damage " + e.name);
    }
}
