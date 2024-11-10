using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackSkill : MonoBehaviour
{
    //setting this need create animation with this too
    public AnimationClip anim;
    protected Entity entity;
    Animator animator;
    public int damage;
    public float cdTime;
    Monster_Behavior monster_Behavior;
    public bool needReload = true;
    void Awake()
    {
        monster_Behavior = GetComponentInParent<Monster_Behavior>();
        animator = monster_Behavior.GetComponent<Animator>();
        entity = monster_Behavior.GetComponent<Entity>();
        entity.SetOutPutDamage(damage);
    }

    public void AttackTrigger()
    {
        if (anim) animator.Play(anim.name);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (monster_Behavior != null)
        {
            if (monster_Behavior.attack || monster_Behavior.isCD)
            {
                return;
            }
            monster_Behavior.attack = true;
            monster_Behavior.SetTarget(other.transform.parent);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {

    }
    public abstract void Attack();

    public abstract void ResetAttack();
}
