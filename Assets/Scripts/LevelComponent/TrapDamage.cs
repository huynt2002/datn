using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDamage : MonoBehaviour
{
    int damage = 10;
    float dameRetrivalTime = 2f;
    float timeCount = 0f;
    Entity entity;
    // Start is called before the first frame update

    public void Set(int damage = 10, float time = 2f)
    {
        this.damage = damage;
        dameRetrivalTime = time;
    }

    void Awake()
    {
        entity = GetComponent<Entity>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeCount > 0)
        {
            timeCount -= Time.deltaTime;
        }
    }

    public bool canDamage()
    {
        return timeCount <= 0;
    }

    public void DamageEntity()
    {
        if (entity)
        {
            entity.TakeDamage(damage, Defines.DamageType.Trap);
            timeCount = dameRetrivalTime;
        }
    }

    public void ResetTimer()
    {
        timeCount = 0;
    }

}
