using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackSkill : MonoBehaviour
{
    //setting this need create animation with this too
    public AnimationClip anim;
    public bool isCD { get; private set; }
    protected Entity entity;
    public int damage;
    public float cdTime;
    float cdCount = 0;
    Monster_Behavior monster_Behavior;
    public bool needWaitOtherAttack = true;
    void Awake()
    {
        monster_Behavior = GetComponentInParent<Monster_Behavior>();
        entity = monster_Behavior.GetComponent<Entity>();
        entity.SetOutPutDamage(damage);
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

    protected void SetAttack(Transform attackTarget)
    {
        monster_Behavior.SetAttack(this);
        cdCount = cdTime;
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
                SetAttack(other.transform.parent);
                return;
            }
            if (monster_Behavior.attack || monster_Behavior.isIdle)
            {
                return;
            }
            SetAttack(other.transform.parent);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {

    }
    public abstract void Attack();

    public abstract void ResetAttack();
}
