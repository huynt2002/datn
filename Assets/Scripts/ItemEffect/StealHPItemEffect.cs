using UnityEngine;

public class StealHPItemEffect : OneTimeEffect
{
    public float stealPercent = 10;
    [Range(0, 100)]
    public float stealChance = 10;
    GameObject effect;
    public override void ApplyEffect()
    {
        var hitBoxs = entity.GetComponentsInChildren<Damage>();
        foreach (var hitbox in hitBoxs)
        {
            if (hitbox.gameObject.GetComponentInChildren<StealHPEffect>())
            {
                return;
            }
            GameObject gameObject = new GameObject("StealHPEffect");
            var component = gameObject.AddComponent<StealHPEffect>();
            gameObject.transform.parent = hitbox.transform;
            gameObject.transform.localPosition = Vector2.zero;
            component.stealChance = stealChance;
            component.stealPercent = stealPercent;
        }
    }

    public override void DestroyEffect()
    {
        Destroy(effect);
        Destroy(gameObject);
    }
}
