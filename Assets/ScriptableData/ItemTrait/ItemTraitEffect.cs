public abstract class ItemTraitEffect : ItemEffect
{
    protected int level;
    public override void ApplyEffect()
    {
        switch (level)
        {
            case 1:
                FirstLevel();
                break;
            case 2:
                SecondLevel();
                break;
            case 3:
                ThirdLevel();
                break;
        }
    }

    public abstract void FirstLevel();

    public abstract void SecondLevel();

    public abstract void ThirdLevel();
    public void SetLevel(int level)
    {
        this.level = level;
    }
}
