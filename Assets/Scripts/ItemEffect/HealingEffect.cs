public class HealingEffect : ItemEffect
{
    public int healNum = 5;
    public override void ApplyEffect()
    {
        if (entity)
        {
            EffectBehaviors.Healing(entity, healNum, transform);
        }
    }
}
