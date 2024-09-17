using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monster_Behavior : MonoBehaviour
{
    protected Animator animator;
    protected Rigidbody2D body;
    protected Entity entity;
    protected GroundSensor groundSensor;
    [Header("Behavior")]
    public DetectArea detectArea;
    public bool canChase;
    public bool canMove;
    public Vector2 moveTarget;
    public int facingDirection;
    public int faceRight;
    public float originCDTime = 1.5f;
    protected float CDTime;
    public float timeCount;
    public bool isCD { get; protected set; }
    [Header("Attack")]
    int currentAttack = 0;
    public bool attack;
    bool attacking = false;
    [SerializeField] GameObject attackSkills;
    public List<GameObject> skills;
    [Header("GetHit")]
    public bool getHit = false;
    public bool canGetHit;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        entity = GetComponent<Entity>();
        groundSensor = GetComponent<GroundSensor>();
        faceRight = (int)transform.localScale.x;
        InitAttack();
        CDTime = originCDTime;
    }

    protected void InitAttack()
    {
        skills = new List<GameObject>();
        var skill = attackSkills.GetComponentsInChildren<AttackSkill>();
        foreach (var x in skill)
        {
            skills.Add(x.gameObject);
            x.gameObject.SetActive(false);
        }
        currentAttack = 0;
        skills[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (getHit)
        {
            GetHit();
        }
        else
        {
            if (!isCD && !getHit)
            {
                if (!attack)
                {
                    Move();
                }
                else
                {
                    Attack();
                }
            }
            else if (isCD)
            {
                animator.SetTrigger("idle");
                timeCount += Time.deltaTime;
                if (timeCount >= CDTime)
                {
                    timeCount = 0;
                    isCD = false;
                    SetupAttack();
                }
            }
        }

    }
    void GetHit()
    {
        attack = false;
        skills[currentAttack].SetActive(false);
        animator.SetTrigger("getHit");
    }
    void Move()
    {
        if (!canMove) return;
        if (detectArea != null && groundSensor.isGrounded)
        {
            if (transform.position.x == moveTarget.x)
            {
                if (!isCD)
                {
                    isCD = true;
                    SetTarget();
                }
            }
            else
            {
                animator.SetTrigger("move");
                Flip(moveTarget);
                transform.position = Vector2.MoveTowards(transform.position,
                            new Vector2(moveTarget.x, transform.position.y), entity.speed * Time.deltaTime);
            }
        }
    }

    public void SkillAttack()
    {
        //in animation
        skills[currentAttack].GetComponent<AttackSkill>().Attack();
    }
    protected void Attack()
    {
        if (!attacking)
        {
            attacking = true;
            Flip(moveTarget);
        }
        skills[currentAttack].GetComponent<AttackSkill>().AttackTrigger();
    }
    protected void Flip(Vector2 moveTarget)
    {
        if (moveTarget.x < transform.position.x)
        {
            facingDirection = -faceRight;
        }
        else if (moveTarget.x > transform.position.x)
        {
            facingDirection = faceRight;
        }
        gameObject.transform.localScale = new Vector3(facingDirection, 1, 1);
    }
    public void CD()
    {
        //reset and CD attack
        skills[currentAttack].GetComponent<AttackSkill>().ResetAttack();
        isCD = true;
        SetCDTime(skills[currentAttack].GetComponent<AttackSkill>().cdTime);
        attack = false;
        attacking = false;
        skills[currentAttack].SetActive(false);
    }

    protected void SetupAttack()
    {
        currentAttack++;
        if (currentAttack >= skills.Count)
        {
            currentAttack = 0;
        }

        skills[currentAttack].SetActive(true);

    }

    public void SetTarget(Transform pos = null)
    {
        Vector3 target;
        if (pos)
        {
            target = pos.position;
        }
        else
        {
            var xSize = detectArea.detectBound.size.x;
            var r = Random.Range((detectArea.transform.position.x - xSize / 2 + 2f),
                (detectArea.transform.position.x + xSize / 2 - 2f));
            target = new Vector3(r, 0, 0);
        }
        moveTarget = target;
    }

    public void ResetGetHit()
    {
        getHit = false;
        skills[currentAttack].SetActive(true);
        animator.SetTrigger("idle");
    }

    protected void SetCDTime(float cd = 0)
    {
        if (cd == 0)
        {
            CDTime = originCDTime;
        }
        else
        {
            CDTime = cd;
        }
    }
}
