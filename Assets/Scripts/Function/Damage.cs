using System.Collections;
using UnityEngine;


public class Damage : MonoBehaviour
{
    protected Entity entity;
    public float knockbackForce;
    protected void Start()
    {
        entity = GetComponentInParent<Entity>();
    }
    protected void KnockBack(Rigidbody2D other)
    {
        if (!other) return;
        Vector2 force = new Vector2(entity.transform.localScale.x * knockbackForce, 0);
        other.AddForce(force, ForceMode2D.Impulse);
    }

    protected void Effect(Vector3 position)
    {
        // Set correct arrow spawn position
        GameObject dust = SpawnManager.instance.SpawnEffect(SpawnManager.EffectType.HitEffect, position + new Vector3(0, 0.5f, 0));
    }

    protected bool getCriticalHit()
    {
        var p = Random.Range(0, 100);
        if (p <= 10)
        {
            return true;
        }
        return false;
    }

    protected float getCriticalHitDamage(float damage)
    {
        return damage * 2;
    }
}
