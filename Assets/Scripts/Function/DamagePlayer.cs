using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : Damage
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Entity target = other.GetComponentInParent<Entity>();
        UpdateDamage();
        if (target.invicible) return;
        target.TakeDamage(damage, damageType, isCritical);
        Effect(Helper.GetPos(target.gameObject));
        var onHitEffects = GetComponents<OnHitEffect>();
        foreach (var onHitEffect in onHitEffects)
        {
            onHitEffect.OnHit(target);
        }
    }
}
