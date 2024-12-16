using System;
using UnityEngine;

[Serializable]
public abstract class ItemEffect : MonoBehaviour
{
    protected Entity entity;
    public abstract void ApplyEffect();
    public abstract void DestroyEffect();
}
