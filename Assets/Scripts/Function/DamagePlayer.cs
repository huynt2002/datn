using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : Damage
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Entity e = other.transform.parent.gameObject.GetComponent<Entity>();
        if (e.invicible) return;
        //deal damage
        e.TakeDamage(entity.outPutDamage, Defines.DamageType.Entity);
        Effect(Helper.GetPos(e.gameObject));
        KnockBack(other.GetComponentInParent<Rigidbody2D>());
        Debug.Log("Damage " + e.name);
    }
}
