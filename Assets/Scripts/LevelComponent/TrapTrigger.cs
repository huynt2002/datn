using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    public int damage;
    public float time;
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
            }
            else
            {
                trapDamage.enabled = true;
            }
            trapDamage?.Set(damage, time);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var go = other.transform.parent.gameObject;
        if (go)
        {
            TrapDamage trapDamage = go.GetComponent<TrapDamage>();
            if (trapDamage) trapDamage.enabled = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {

    }
}
