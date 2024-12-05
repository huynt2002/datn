using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnemy : Damage
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Entity target = other.GetComponentInParent<Entity>();
        UpdateDamage();
        GetCriticalHit();
        if (target.isAlive)
        {
            //deal damage
            Effect(Helper.GetPos(target.gameObject));
            if (target.invicible) { return; }
            target.TakeDamage(damage, damageType, isCritical);
            var onHitEffects = GetComponents<OnHitEffect>();
            foreach (var onHitEffect in onHitEffects)
            {
                onHitEffect.OnHit(target);
            }
        }
    }
}
