using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemTraitEffect : ItemEffect
{
    public int level;
    public override void ApplyEffect()
    {

    }

    public abstract void FirstLevel();

    public abstract void SecondLevel();

    public abstract void ThirdLevel();
}
