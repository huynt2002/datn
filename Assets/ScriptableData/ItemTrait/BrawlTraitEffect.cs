public class BrawlTraitEffect : ItemTraitEffect
{
    public override bool Check()
    {
        return true;
    }
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
            if (activePercent > 50) return;
            EffectBehaviors.Healing(entity, 5, transform);
        }
    }

    public override void SecondLevel()
    {

    }

    public override void ThirdLevel()
    {

    }
}
