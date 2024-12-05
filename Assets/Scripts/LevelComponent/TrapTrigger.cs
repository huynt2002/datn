using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    [SerializeField] int damage = 10;
    [SerializeField] float time = 1.5f;
    [SerializeField] float knockForce = 10f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var go = other.transform.parent.gameObject;
        if (go)
        {
            TrapDamage trapDamage = go.GetComponent<TrapDamage>();
            if (!trapDamage)
            {
                go.AddComponent<TrapDamage>();
                trapDamage = go.GetComponent<TrapDamage>();
                trapDamage.Set(damage, time);
                DamageEntity(trapDamage, go.GetComponent<Rigidbody2D>());
            }
            else
            {
                DamageEntity(trapDamage, go.GetComponent<Rigidbody2D>());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var go = other.transform.parent.gameObject;
        if (go)
        {
            TrapDamage trapDamage = go.GetComponent<TrapDamage>();
            if (trapDamage)
            {
                trapDamage.ResetTimer();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        var go = other.transform.parent.gameObject;
        if (go)
        {
            if (!go.GetComponent<Entity>().isAlive)
            {
                return;
            }
            TrapDamage trapDamage = go.GetComponent<TrapDamage>();
            DamageEntity(trapDamage, go.GetComponent<Rigidbody2D>());
        }
    }

    void DamageEntity(TrapDamage trapDamage, Rigidbody2D rg)
    {
        if (trapDamage == null || rg == null) return;
        if (trapDamage.canDamage())
        {
            rg.velocity = Vector2.zero;
            rg.AddForce(Vector2.up * knockForce, ForceMode2D.Impulse);
            trapDamage.DamageEntity();
        }
    }
}
