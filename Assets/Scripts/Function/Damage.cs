using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Damage : MonoBehaviour
{
    protected Entity entity;
    public float knockbackForce;
    protected void Start()
    {
        entity = transform.parent?.gameObject.GetComponent<Entity>();
    }
    protected void KnockBack(Rigidbody2D other)
    {
        Vector2 force = new Vector2(entity.transform.localScale.x * knockbackForce, 0);
        other.AddForce(force, ForceMode2D.Impulse);
    }
    protected void Effect(Vector3 position)
    {
        // Set correct arrow spawn position
        GameObject dust = SpawnManager.instance.SpawnEffect(SpawnManager.EffectType.HitEffect, position + new Vector3(0, 0.5f, 0)) as GameObject;
    }
}
