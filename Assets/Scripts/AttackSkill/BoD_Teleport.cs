using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoD_Teleport : AttackSkill
{
    public override void Attack()
    {
        entity.SetInvincible(true);
    }

    public override void ResetAttack()
    {
        entity.SetInvincible(false);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
