public abstract class ItemTraitEffect : ItemEffect
{
    protected int level;
    public override void ApplyEffect()
    {

    }

    public abstract void FirstLevel();

    public abstract void SecondLevel();

    public abstract void ThirdLevel();
    public void SetLevel(int level)
    {
        this.level = level;
    }
}
