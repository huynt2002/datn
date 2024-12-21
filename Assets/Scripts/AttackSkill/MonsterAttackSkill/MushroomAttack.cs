using UnityEngine;

public class MushroomAttack : MonsterAttackSkill
{
    [SerializeField] GameObject projectile;
    [SerializeField] Transform pos;
    public override void OnAttacking()
    {
        if (projectile != null)
        {
            var go = Instantiate(projectile, pos.position, Quaternion.identity);
            go.GetComponent<Projectile>().SetUp(totalDamage, entity.transform.localScale.x, gameObject.layer);
        }
        else
        {
            Debug.LogError("Projectile Null");
        }
    }

    public override void ResetAttack()
    {
    }
}
