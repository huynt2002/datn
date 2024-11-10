public class BrawlTraitEffect : ItemTraitEffect
{
    public override void FirstLevel()
    {
        if (entity.getHit)
        {
            var activePercent = UnityEngine.Random.Range(0, 100);
            if (activePercent > 50) return;
            EffectBehaviors.Healing(entity, 5, transform);
            ResetCD();
        }
    }

    public override void SecondLevel()
    {
        if (entity.getHit)
        {
            var activePercent = UnityEngine.Random.Range(0, 100);
            if (activePercent > 50) return;
            EffectBehaviors.Healing(entity, 10, transform);
            ResetCD();
        }
    }

    public override void ThirdLevel()
    {

    }
}
