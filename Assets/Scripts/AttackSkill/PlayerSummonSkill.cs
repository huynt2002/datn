using System.Collections.Generic;
using UnityEngine;

public class PlayerSummonSkill : AttackSkill
{
    [SerializeField] List<GameObject> monsters;
    [SerializeField] Transform pos;
    public override void OnAttacking()
    {
        var randomMonster = Random.Range(0, monsters.Count);
        var go = SpawnManager.instance.SpawnMonster(monsters[randomMonster], pos.position, GameObject.FindGameObjectWithTag(Defines.Tag.StateLevel).transform, Defines.MonsterType.Ally);
    }

    public override void ResetAttack()
    {

    }
}
