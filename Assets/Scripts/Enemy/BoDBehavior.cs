using System.Collections;
using UnityEngine;

public class BoDBehavior : Monster_Behavior
{
    [Header("Move")]
    [SerializeField] AttackSkill teleport;
    [SerializeField] Transform pos1;
    [SerializeField] Transform pos2;
    [SerializeField] Transform pPos1;
    [SerializeField] Transform pPos2;
    [Header("Attack")]
    [SerializeField] AttackSkill normalAttack;
    [SerializeField] AttackSkill magicAttack;
    [SerializeField] AttackSkill comboAttack;
    [SerializeField] AttackSkill projectileAttack;
    Transform playerPos;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        entity = GetComponent<Entity>();
        groundSensor = GetComponent<GroundSensor>();
        ChoseAttack();
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = PlayerStats.instance.transform;
        Attack();
    }

    void Teleport()
    {
        teleport?.Attack();
        StartCoroutine(DisableInvicible(teleport.anim.length));
    }

    IEnumerator DisableInvicible(float length)
    {
        yield return new WaitForSeconds(length);
        teleport?.ResetAttack();
    }

    public void SkillTrigger()
    {
        currentAttackSkill?.Attack();
    }

    public void SkillTriggerFlip()
    {
        Flip(playerPos.position);
        currentAttackSkill?.Attack();
    }

    void ChoseAttack()
    {
        var tmp = Random.Range(0, 100);
        if (tmp >= 60)
        {
            currentAttackSkill = normalAttack;
            if (entity.currentHP / entity.maxHP <= 0.5) currentAttackSkill = comboAttack;
        }
        else if (tmp >= 40)
        {
            currentAttackSkill = projectileAttack;
        }
        else if (tmp >= 20)
        {
            currentAttackSkill = magicAttack;
        }
        else
        {
            currentAttackSkill = comboAttack;
            if (entity.currentHP / entity.maxHP > 0.5) currentAttackSkill = normalAttack;
        }
    }
    public void CoolDown()
    {
        currentAttackSkill?.ResetAttack();
        Teleport();
        isIdle = true;
        ChoseAttack();
    }

    void TeleportToPos()
    {
        var r = Random.Range(0, 100);
        if (r >= 50)
        {
            transform.position = pos1.position;
        }
        else
        {
            transform.position = pos2.position;
        }
        Flip(playerPos.position);
    }

    void TeleportToPlayer()
    {
        if (entity.currentHP / entity.maxHP > 0.5)
        {
            if (PlayerStats.instance.transform.localScale.x == 1)
            {
                transform.position = pPos1.position;
            }
            else transform.position = pPos2.position;
        }
        else
        {
            var r = Random.Range(0, 100);
            if (r >= 50)
            {
                transform.position = pPos1.position;
            }
            else
            {
                transform.position = pPos2.position;
            }
        }
        Flip(playerPos.position);
    }
    private void OnDestroy()
    {
        GameManager.instance.isBossFight = false;
        SoundManager.instance.PlayMainMusic();
    }
}


