using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoDMagicAttack : AttackSkill
{
    Vector2 pPos;
    [SerializeField] GameObject boDProjectile;
    List<Vector2> spawnPosList;
    public override void Attack()
    {
        entity.SetOutPutDamage(damage);
        pPos = new Vector2(PlayerStats.instance.transform.position.x, PlayerStats.instance.transform.position.y + 2f);
        NormalMagic();
    }

    public override void ResetAttack()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        spawnPosList = new List<Vector2>();
    }

    void NormalMagic()
    {
        int tmp = 1;
        for (int i = 0; i < 2; i++)
        {
            Vector2 t = new Vector2(pPos.x - tmp, pPos.y);
            tmp = tmp - 2;
            spawnPosList.Add(t);
        }
        var go = Instantiate(boDProjectile, pPos, Quaternion.identity) as GameObject;
        go.GetComponent<Projectile>()?.SetUp(entity.outputDamage, entity.transform.localScale.x, gameObject.layer);
        foreach (var i in spawnPosList)
        {
            var go1 = Instantiate(boDProjectile, i, Quaternion.identity) as GameObject;
            go1.GetComponent<Projectile>()?.SetUp(entity.outputDamage, entity.transform.localScale.x, gameObject.layer);
        }
        spawnPosList.Clear();
    }
}
