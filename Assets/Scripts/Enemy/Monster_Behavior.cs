using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monster_Behavior : MonoBehaviour
{
    protected Animator animator;
    [SerializeField] AnimationController animationController;
    protected Rigidbody2D body;
    protected Entity entity;
    protected GroundSensor groundSensor;
    [Header("Behavior")]
    public DetectArea detectArea;
    public bool canChase;
    bool chasingPlayer;
    public bool canMove;
    public bool canGetHit;
    public bool runWhenCD;
    public Transform attackTarget { get; private set; }
    public Vector2 moveTarget;
    public int facingDirection { get; private set; }
    public int faceRight { get; private set; }
    public bool isIdle;
    [SerializeField] protected float idleTime = 1f;
    float idleCount = 0;
    [Header("Attack")]
    public bool attack;
    public AttackSkill currentAttackSkill = null;
    [SerializeField] float getHitTime = 0.15f;
    public Defines.MonsterType monsterType;
    [SerializeField] Collider2D entityCollider;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        entity = GetComponent<Entity>();
        groundSensor = GetComponent<GroundSensor>();
        faceRight = (int)transform.localScale.x;
        SetIdle(true);
        moveTarget = transform.position;

        var skills = GetComponentsInChildren<AttackSkill>();

        switch (monsterType)
        {
            case Defines.MonsterType.Enemy:
                foreach (var skill in skills)
                {
                    LayerMask layer = LayerMask.NameToLayer(Defines.DetectType.DetectPlayer.ToString());
                    skill.gameObject.layer = layer;
                    foreach (var child in skill.GetComponentsInChildren<Collider2D>())
                    {
                        child.gameObject.layer = layer;
                    }
                }
                entityCollider.tag = Defines.Tag.Enemy;
                entityCollider.gameObject.layer = LayerMask.NameToLayer(Defines.Tag.Enemy);
                break;
            case Defines.MonsterType.Ally:
                foreach (var skill in skills)
                {
                    LayerMask layer = LayerMask.NameToLayer(Defines.DetectType.DetectEnemy.ToString());
                    skill.gameObject.layer = layer;
                    foreach (var child in skill.GetComponentsInChildren<Collider2D>())
                    {
                        child.gameObject.layer = layer;
                    }
                }
                entityCollider.tag = Defines.Tag.Ally;
                entityCollider.gameObject.layer = LayerMask.NameToLayer(Defines.Tag.Ally);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!entity.isAlive)
        {
            animationController.PlayDeadAnimation();
            return;
        }

        Air();
        GetHit();
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
        else
        {
            Idle();
        }
    }

    void FixedUpdate()
    {
        if (idleCount > 0)
        {
            idleCount -= Time.deltaTime;
        }
        else
        {
            isIdle = false;
        }
    }

    void Air()
    {
        if (!groundSensor.isGrounded)
        {
            animationController.PlayJumpAnimation();
            return;
        }
    }

    void Idle()
    {
        if (!groundSensor.isGrounded || attack || (entity.getHit && canGetHit))
        {
            return;
        }
        if (isIdle)
        {
            animationController.PlayIdleAnimation();
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
        if (!entity.getHit || !canGetHit || attack)
        {
            return;
        }
        ResetAttack();
        StartCoroutine(GetHitPerform());
    }

    IEnumerator GetHitPerform()
    {
        animationController.PlayGetHitAnimation();
        yield return new WaitForSeconds(getHitTime);
        entity.ResetGetHit();
        SetIdle(true);
    }

    void Move()
    {
        if (!canMove) { return; }
        if (isIdle) { return; }
        if (detectArea != null && groundSensor.isGrounded)
        {
            if (canChase) { ChasePlayer(); }
            if (Helper.RoundingFloatNumber(transform.position.x) == Helper.RoundingFloatNumber(moveTarget.x))
            {
                SetRandomMovePos();
                SetIdle(true);
                return;
            }
            if (chasingPlayer)
            {
                MoveToPos(attackTarget.position, entity.speed * 1.2f);
                return;
            }

            MoveToPos(moveTarget, entity.speed);
        }
    }

    void MoveToPos(Vector2 moveTarget, float speed)
    {
        animationController.PlayMoveAnimation();
        Flip(moveTarget);
        transform.position = Vector2.MoveTowards(transform.position,
                               new Vector2(moveTarget.x, transform.position.y), speed * Time.deltaTime);

    }

    void ChasePlayer()
    {
        if (!attackTarget)
        {
            chasingPlayer = false;
            return;
        }
        if (currentAttackSkill)
        {
            if (currentAttackSkill.isCD && runWhenCD)
            {
                chasingPlayer = false;
                return;
            }
        }
        chasingPlayer = true;
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
        //Flip(playerTransform.position);
        currentAttackSkill?.Attack();
    }

    public void SetAttack(AttackSkill attackSkill, Transform target)
    {
        attack = true;
        Flip(target.position);
        currentAttackSkill = attackSkill;
    }

    public void ResetAttack()
    {
        attack = false;
    }

    public void ResetAttackWithIdle()
    {
        attack = false;
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

    public void SetAttackTarget(Transform player)
    {
        attackTarget = player;
    }

    void SetRandomMovePos()
    {
        var xSize = detectArea.detectBound.size.x;
        var r = Random.Range((detectArea.transform.position.x - xSize / 2 + 2f),
            (detectArea.transform.position.x + xSize / 2 - 2f));
        Vector2 target = new Vector3(r, 0, 0);
        moveTarget = target;
    }
}
