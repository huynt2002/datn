public class HealingEffect : MultiTimeEffect
{
    public int healNum = 5;
    public override void ApplyEffect()
    {
        if (entity)
        {
            EffectBehaviors.Healing(entity, healNum, transform);
            ResetCD();
        }
    }
    public override void DestroyEffect()
    {
        Destroy(gameObject);
    }
}
