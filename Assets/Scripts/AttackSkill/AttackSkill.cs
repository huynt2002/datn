using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackSkill : MonoBehaviour
{
    //setting this need create animation with this too
    public AnimationClip anim;
    public bool isCD { get; private set; }
    protected Entity entity;
    public float damageMultiple = 1;
    protected float totalDamage => damageMultiple * entity.damage;
    public float cdTime;
    public float cdCount { get; protected set; }
    public bool needWaitOtherAttack = true;
    public string skillName;
    public string skillDescription;
    protected void Awake()
    {
        entity = GetComponentInParent<Entity>();
    }

    void Update()
    {
        if (cdCount > 0)
        {
            isCD = true;
            cdCount -= Time.deltaTime;
        }
        else
        {
            isCD = false;
        }
    }

    public void Attack()
    {
        cdCount = cdTime;
    }
    public abstract void OnAttacking();

    public abstract void ResetAttack();
}
