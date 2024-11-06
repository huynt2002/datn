public class BrawlTraitEffect : ItemTraitEffect
{
    public override void ApplyEffect()
    {
        switch (level)
        {
            case 0:
                FirstLevel();
                break;
            case 1:
                SecondLevel();
                break;
            case 2:
                ThirdLevel();
                break;
        }
    }

    public override void FirstLevel()
    {
        if (entity.getHit)
        {
            var activePercent = UnityEngine.Random.Range(0, 100);
            if (activePercent > 80) return;
            EffectBehaviors.Healing(entity, 5, transform);
            ResetCD();
        }
    }

    public override void SecondLevel()
    {

    }

    public override void ThirdLevel()
    {

    }
}
