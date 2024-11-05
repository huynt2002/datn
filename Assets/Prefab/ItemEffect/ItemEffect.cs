using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemEffect : MonoBehaviour
{
    public abstract bool Check();
    public abstract void ApplyEffect();

    void Update()
    {
        if (Check())
        {
            ApplyEffect();
        }
    }
}
