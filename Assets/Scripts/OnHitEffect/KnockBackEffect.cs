using UnityEngine;

public class KnockBackEffect : OnHitEffect
{
    public float knockbackForce;
    public override void OnHit(Entity target, Entity dealer = null, float damage = 0)
    {
        KnockBack(target.GetComponent<Rigidbody2D>());
    }

    protected void KnockBack(Rigidbody2D target)
    {
        if (!target) return;
        Vector2 force = new Vector2(transform.lossyScale.x * knockbackForce, 0);
        target.AddForce(force, ForceMode2D.Impulse);
    }
}
