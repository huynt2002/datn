using UnityEngine;

public abstract class OnHitEffect : MonoBehaviour
{
    public abstract void OnHit(Entity target, Entity dealer = null, float damage = 0f);
}
