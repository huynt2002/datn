using UnityEngine;

public class WizardAttack : MonsterAttackSkill
{
    [SerializeField] GameObject projectile;
    public float ranThresh;
    public override void OnAttacking()
    {
        var x = Random.Range(transform.position.x - ranThresh, transform.position.x + ranThresh);
        var y = Random.Range(transform.position.y - ranThresh, transform.position.y + ranThresh);
        var go = Instantiate(projectile, new Vector2(x, y), Quaternion.identity) as GameObject;
        go.GetComponent<Projectile>().SetUp(totalDamage, entity.transform.localScale.x, gameObject.layer);
    }

    public override void ResetAttack()
    {

    }
}
