using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    Entity entity;
    [Header("Components")]
    [SerializeField] Rigidbody2D body;
    [SerializeField] PlayerAnimationController animatorController;
    [SerializeField] GroundSensor groundSensor;
    bool isGrounded => groundSensor.isGrounded;
    [SerializeField] GameObject playerCollider;
    [Header("Move")]
    [SerializeField] bool isMove;
    [SerializeField] float speed = 3f;
    float horizontal;
    int facingDirection = 1;
    public bool isOneWay;
    [Header("Jump")]
    [SerializeField] float jumpForce = 5f;
    [SerializeField] int maxJump = 1;
    int remainingJump = 0;

    [Header("Dash")]
    bool canDash = true;
    [SerializeField] float dashSpeed = 20f;
    [SerializeField] float dashDuration = 1f;
    //public float dashDurationS = 0;
    [SerializeField] float dashCD = 1f;
    //public float dashCDS = 0;
    [SerializeField] bool isDash = false;
    //public int maxDash = 2;
    //public int remainDash = 0;

    [Header("Attack")]

    private int comboStep = 0;
    private float comboTimer = 0f;
    [SerializeField] float comboDelay = 0.4f;
    [SerializeField] int maxComboStep = 3;
    [SerializeField] bool isAttack = false;


    [Header("Interact")]
    public PlayerInteract playerInteract;
    [Header("GetHit")]
    [SerializeField] float getHitTime = 0.15f;
    bool getHit => entity.getHit;

    [Header("Skill")]
    [SerializeField] bool isSkill;
    public float skillCDTime = 3f;
    bool canUseSkill;
    public float cdCount { get; private set; } = 0;
    [SerializeField] GameObject skillPre;
    [SerializeField] Transform skillPos;
    // Start is called before the first frame update

    void Awake()
    {
        instance = this;
        entity = GetComponent<Entity>();
        animatorController = GetComponent<PlayerAnimationController>();
    }

    // Update is called once per frame
    void Update()
    {
        SkillCD();
        GetHit();
        Air();
        Move();
        Idle();
    }

    void FixedUpdate()
    {
        UpdateComboAttackTimer();
    }

    void GetHit()
    {
        if (getHit)
        {
            StartCoroutine(PlayerGetHit());
        }
    }

    IEnumerator PlayerGetHit()
    {
        animatorController.PlayGetHitAnimation();
        ResetSkill();
        yield return new WaitForSeconds(getHitTime);
        entity.ResetGetHit();
    }

    void Idle()
    {
        if (isMove || !isGrounded || getHit || isDash || isSkill || isAttack)
        {
            return;
        }

        animatorController.PlayIdleAnimation();
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (!isGrounded || isDash || isSkill || getHit)
        {
            return;
        }
        if (context.performed)
        {
            StartCoroutine(AttackPerform());
        }
    }

    IEnumerator AttackPerform()
    {
        if (isAttack)
        {
            yield return new WaitForSeconds(comboDelay / 4f);
        }
        if (comboStep > 2)
        {
            yield return new WaitForSeconds(comboStep / 3f);
        }
        if (comboTimer > 0)
        {
            comboStep++;
            if (comboStep > maxComboStep)
                comboStep = 1;
        }
        else
        {
            comboStep = 1;
        }
        isAttack = true;
        comboTimer = comboDelay;
        animatorController.PlayAttackAnimation(comboStep);
    }

    void UpdateComboAttackTimer()
    {
        if (comboTimer > 0)
        {
            comboTimer -= Time.deltaTime;
        }
        else
        {
            comboStep = 0;
            isAttack = false;
        }
    }

    void SkillCD()
    {
        if (!canUseSkill)
        {
            if (cdCount > 0)
            {
                cdCount -= Time.deltaTime;
            }
            else
            {
                cdCount = 0;
                canUseSkill = true;
            }
        }
    }

    public void SkillAttackInput(InputAction.CallbackContext context)
    {
        if (!canUseSkill || getHit) { return; }
        if (context.performed)
        {
            isSkill = true;
            canUseSkill = false;
            cdCount = skillCDTime;
            animatorController.PlaySkillAnimation();
        }
    }

    public void SkillAttack()
    {
        var go = Instantiate(skillPre, skillPos.position, Quaternion.identity) as GameObject;
        go.GetComponent<ProjectileBehavior>()?.Set(ProjectileBehavior.Target.Enemy, 2 * GetComponent<Entity>().outPutDamage);
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (context.performed && canDash)
        {
            StartCoroutine(PerformDash());
        }
    }
    IEnumerator PerformDash()
    {
        animatorController.PlayDashAnimation();
        Flip();
        ResetSkill();
        canDash = false;
        isDash = true;
        DashEffect();
        SoundManager.instance.PlayDashSound(gameObject.transform);
        body.gravityScale = 0;
        body.velocity = new Vector2(dashSpeed * facingDirection, 0f);
        PlayerStats.instance.SetInvincible(true);
        yield return new WaitForSeconds(dashDuration);
        body.gravityScale = Defines.Physics.GravityScale;
        body.velocity = new Vector2(0f, body.velocity.y);
        PlayerStats.instance.SetInvincible(false);
        isDash = false;
        yield return new WaitForSeconds(dashCD);
        canDash = true;
    }

    void Air()
    {
        if (isGrounded)
        {
            remainingJump = maxJump;
        }
        if (isDash || isSkill || isGrounded || getHit)
        {
            return;
        }
        animatorController.PlayJumpAnimation();
    }
    private void Move()
    {
        if (isDash || isSkill || getHit || isAttack) { return; }
        if (horizontal != 0)
        {
            isMove = true;
            body.velocity = new Vector2(horizontal * speed, body.velocity.y);
            if (isGrounded) { animatorController.PlayMoveAnimation(); }
            Flip();
        }
        else
        {
            isMove = false;
        }
    }
    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (remainingJump > 0)
            {
                SoundManager.instance.PlayJumpSound(gameObject.transform);
                if (!isGrounded)
                {
                    Vector2 pos = new Vector2(
                            gameObject.transform.position.x, gameObject.transform.position.y - 1f);
                    GameObject effect = SpawnManager
                        .instance
                        .SpawnEffect(SpawnManager.EffectType.PlayerJumpEffect, pos);
                }
                ResetSkill();
                body.velocity = new Vector2(body.velocity.x, jumpForce);
                remainingJump--;
            }
        }
    }



    void DashEffect()
    {
        // Set correct arrow spawn position
        GameObject dust = SpawnManager.instance.SpawnEffect(SpawnManager.EffectType.PlayerDashEffect, gameObject.transform.position);
        // Turn arrow in correct direction
        if (dust)
        {
            dust.transform.localScale = new Vector3(facingDirection, 1, 1);
        }
    }

    void Flip()
    {
        if (horizontal < 0)
        {
            facingDirection = -1;
        }
        else if (horizontal > 0)
        {
            facingDirection = 1;
        }
        gameObject.transform.localScale = new Vector3(facingDirection, 1, 1);
    }

    public void ResetSkill()
    {
        if (isSkill)
        {
            isSkill = false;
        }
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (playerInteract)
        {
            switch (playerInteract.interactType)
            {
                case PlayerInteract.InteractType.None:
                    break;
                case PlayerInteract.InteractType.Gate:
                    playerInteract.Gate();
                    break;
                case PlayerInteract.InteractType.Item:
                    playerInteract.Item();
                    break;
                case PlayerInteract.InteractType.NPC:
                    playerInteract.NPC();
                    break;
                case PlayerInteract.InteractType.Chest:
                    playerInteract.Chest();
                    break;
            }
        }
    }

    public void OneWayPlatform(InputAction.CallbackContext context)
    {
        if (groundSensor.platformCollider)
        {
            foreach (var i in playerCollider.GetComponents<Collider2D>()) Physics2D.IgnoreCollision(i, groundSensor.platformCollider);
            isOneWay = true;
        }
    }
}
