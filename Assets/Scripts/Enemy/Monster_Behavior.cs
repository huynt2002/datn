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
    public bool isIdle { get; protected set; }
    public float idleTime = 1f;
    float idleCount = 0;
    [Header("Attack")]
    public bool attack;
    public AttackSkill currentAttackSkill = null;
    [Header("GetHit")]
    public bool canGetHit;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        entity = GetComponent<Entity>();
        groundSensor = GetComponent<GroundSensor>();
        faceRight = (int)transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (!entity.IsAlive)
        {
            animator.SetTrigger("dead");
            return;
        }
        if (entity.getHit && canGetHit)
        {
            GetHit();
        }
        else
        {
            if (!isIdle && (!entity.getHit || !canGetHit))
            {
                if (attack)
                {
                    Attack();
                }
                else
                {
                    Move();
                }
            }
            else if (isIdle)
            {
                Idle();
            }
        }

    }

    void Idle()
    {
        animator.SetTrigger("idle");
        if (idleCount > 0)
        {
            idleCount -= Time.deltaTime;
        }
        else
        {
            isIdle = false;
        }
    }

    public void SetIdle(bool set)
    {
        isIdle = set;
        if (set)
        {
            idleCount = idleTime;
        }
        else
        {
            idleCount = 0;
        }

    }

    void GetHit()
    {
        ResetAttack();
        animator.SetTrigger("getHit");
    }
    void Move()
    {
        if (!canMove) return;
        if (detectArea != null && groundSensor.isGrounded)
        {
            if (transform.position.x == moveTarget.x)
            {
                if (!isIdle)
                {
                    SetIdle(true);
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

    protected void Attack()
    {
        if (currentAttackSkill)
        {
            animator.Play(currentAttackSkill.anim.name);
        }
    }

    public void AttackInAnimation()
    {
        //only trigger in animation
        Flip(moveTarget);
        currentAttackSkill?.Attack();
    }

    public void SetAttack(AttackSkill attackSkill, Transform target)
    {
        attack = true;
        currentAttackSkill = attackSkill;
        SetTarget(target);
    }

    public void ResetAttack()
    {
        attack = false;
    }

    public void ResetAttackWithIdle()
    {
        ResetAttack();
        SetIdle(true);
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
        entity.ResetGetHit();
        animator.SetTrigger("idle");
    }
}
