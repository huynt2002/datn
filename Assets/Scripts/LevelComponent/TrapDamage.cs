using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDamage : MonoBehaviour
{
    public int damage = 10;
    public float dameRetrivalTime = 3f;
    float timeCount = 0f;
    Entity entity;
    // Start is called before the first frame update

    public void Set(int damage = 10, float time = 1f)
    {
        this.damage = damage;
        dameRetrivalTime = time;
    }

    void Awake()
    {
        entity = GetComponent<Entity>();
        entity.TakeDamage(damage, Defines.DamageType.Trap);
        timeCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (entity)
        {
            timeCount += Time.deltaTime;
            if (timeCount > dameRetrivalTime)
            {
                entity.TakeDamage(damage, Defines.DamageType.Trap);
                timeCount = 0;
            }
        }
    }
}
