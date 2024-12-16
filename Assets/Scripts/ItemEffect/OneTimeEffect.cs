using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTimeEffect : ItemEffect
{
    void Start()
    {
        entity = gameObject.GetComponentInParent<Entity>();
        ApplyEffect();
    }
    public override void ApplyEffect()
    {

    }

    public override void DestroyEffect()
    {

    }
}
