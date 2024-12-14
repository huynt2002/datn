using UnityEngine;

public abstract class MonsterAttackSkill : AttackSkill
{
    Monster_Behavior monster_Behavior;

    private void Awake()
    {
        base.Awake();
        monster_Behavior = GetComponentInParent<Monster_Behavior>();
    }

    protected void SetMonsterAttack(Transform attackTarget)
    {
        monster_Behavior.SetAttack(this, attackTarget);
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (cdCount > 0)
        {
            return;
        }
        if (monster_Behavior != null)
        {
            if (!needWaitOtherAttack)
            {
                monster_Behavior.SetIdle(false);
                monster_Behavior.ResetAttack();
                SetMonsterAttack(other.transform.parent);
                return;
            }
            if (monster_Behavior.attack || monster_Behavior.isIdle)
            {
                if (monster_Behavior.currentAttackSkill)
                {
                    if (!monster_Behavior.currentAttackSkill.isCD)
                    {
                        return;
                    }
                }
            }
            SetMonsterAttack(other.transform.parent);
        }
    }
}
