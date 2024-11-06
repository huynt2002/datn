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

    AttackSkill currentSkill;
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
        if (isCD)
        {
            timeCount += Time.deltaTime;
            if (timeCount >= CDTime)
            {
                timeCount = 0;
                isCD = false;
            }
        }
        else
        {
            BoDAttack();
        }
    }

    void Teleport()
    {
        teleport?.AttackTrigger();
        teleport?.Attack();
        StartCoroutine(DisableInvicible(teleport.anim.length));
    }

    IEnumerator DisableInvicible(float length)
    {
        yield return new WaitForSeconds(length);
        teleport?.ResetAttack();
    }

    void BoDAttack()
    {
        currentSkill?.AttackTrigger();
    }

    public void SkillTrigger()
    {
        currentSkill?.Attack();
    }

    public void SkillTriggerFlip()
    {
        Flip(playerPos.position);
        currentSkill?.Attack();
    }

    void ChoseAttack()
    {
        var tmp = Random.Range(0, 100);
        if (tmp >= 60)
        {
            currentSkill = normalAttack;
            if (entity.CurrentHP / entity.MaxHP <= 0.5) currentSkill = comboAttack;
        }
        else if (tmp >= 40)
        {
            currentSkill = projectileAttack;
        }
        else if (tmp >= 20)
        {
            currentSkill = magicAttack;
        }
        else
        {
            currentSkill = comboAttack;
            if (entity.CurrentHP / entity.MaxHP > 0.5) currentSkill = normalAttack;
        }
    }
    public void CoolDown()
    {
        currentSkill?.ResetAttack();
        SetCDTime(currentSkill.cdTime + teleport.anim.length);
        Teleport();
        isCD = true;
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
        if (entity.CurrentHP / entity.MaxHP > 0.5)
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


