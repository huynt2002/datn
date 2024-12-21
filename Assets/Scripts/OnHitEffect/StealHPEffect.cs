using UnityEngine;

public class StealHPEffect : OnHitEffect
{
    public float stealPercent = 10;
    [Range(0, 100)]
    public float stealChance = 10;
    [Range(0, 100)]
    public int hpThresh = 50;
    public override void OnHit(Entity target, Entity dealer = null, float damage = 0)
    {
        var rand = Random.Range(0, 100);
        if (rand > stealChance)
        {
            return;
        }
        if (dealer)
        {
            if (dealer.currentHP / dealer.maxHP >= hpThresh)
            {
                return;
            }
            var healAmount = damage * stealPercent / 100;
            if (healAmount > 0)
            {
                dealer.HealHP(healAmount);
                return;
            }
            dealer.HealHP(1);
        }
    }
}
