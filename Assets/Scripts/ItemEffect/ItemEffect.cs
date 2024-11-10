using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemEffect : MonoBehaviour
{
    protected Entity entity;
    public float effectCD = 5f;
    protected float countCD = 0;
    public virtual bool Check()
    {
        if (countCD < effectCD)
        {
            countCD += Time.deltaTime;
            return false;
        }
        return true;
    }

    public void ResetCD(float cd = 0f)
    {
        countCD = cd;
    }

    public abstract void ApplyEffect();

    protected void Start()
    {
        entity = gameObject.GetComponentInParent<Entity>();
    }
    void Update()
    {
        if (Check())
        {
            ApplyEffect();
        }
    }
}
