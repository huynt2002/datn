using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OnHitEffect : MonoBehaviour
{
    public abstract void OnHit(Entity target);
}
