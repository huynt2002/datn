using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnemy : Damage
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Entity e = other.transform.parent.gameObject.GetComponent<Entity>();
        if (e.IsAlive)
        {
            //deal damage
            Effect(e.gameObject.transform.position);
            if (e.invicible) return;
            e.getHit = true;
            if (getCriticalHit())
            {
                e.TakeDamage(getCriticalHitDamage(entity.outPutDamage), Defines.DamageType.Entity, true);
            }
            else { e.TakeDamage(entity.outPutDamage, Defines.DamageType.Entity); }

            KnockBack(e.GetComponent<Rigidbody2D>());
            Debug.Log("Damage " + e.name);
        }
    }
}
