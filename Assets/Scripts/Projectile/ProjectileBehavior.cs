using System;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public enum ProjectileType
    {
        Chase, BoD, Fireball
    }

    public enum Target
    {
        Player, Enemy
    }

    [SerializeField] ProjectileType type;
    Vector2 originPos;
    Vector2 goalPos;
    Transform targetPos;
    float speed;
    float damage;
    public bool destroyedAtCollided = false;
    Target target;
    public float nearestDistanceSqr = 30f;
    // Start is called before the first frame update
    [SerializeField]
    public void Set(Target target, float damage = 10, float speed = 7f)
    {
        this.target = target;
        this.damage = damage;
        this.speed = speed;
        GetGoalPos();
    }

    void Awake()
    {
        gameObject.SetActive(false);
    }

    void GetGoalPos()
    {
        if (type == ProjectileType.Chase)
        {
            targetPos = GetTargetTransform();
            if (targetPos)
            {
                ToTargetPos(targetPos.position);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else if (type == ProjectileType.BoD)
        {

        }
        else if (type == ProjectileType.Fireball)
        {
            StraightWay();
        }
        gameObject.SetActive(true);
    }

    void ToTargetPos(Vector2 targetPos)
    {
        originPos = transform.position;
        Vector2 direction = targetPos - originPos;
        direction = new Vector2(10f * direction.x, 10f * direction.y);
        goalPos = originPos + direction;
    }

    void StraightWay()
    {
        originPos = transform.position;
        goalPos = new Vector2(originPos.x + 1 * PlayerStats.instance.transform.localScale.x, originPos.y);
        Vector2 direction = goalPos - originPos;
        direction = new Vector2(30f * direction.x, 30f * direction.y);
        goalPos = originPos + direction;
    }

    // Update is called once per frame
    void Update()
    {
        if (type == ProjectileType.Chase)
        {
            Chase();
        }
        else if (type == ProjectileType.BoD)
        {
            BoD();
        }
        else if (type == ProjectileType.Fireball)
        {
            Chase();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.transform.parent.GetComponent<Entity>().TakeDamage(damage, Defines.DamageType.Projectile);
        if (destroyedAtCollided)
        {
            Destroy(gameObject);
        }
    }

    public void Chase()
    {
        if ((Vector2)transform.position == goalPos) Destroy(gameObject);
        transform.position = Vector2.MoveTowards(transform.position, goalPos, speed * Time.deltaTime);
        Vector3 moveDirection = -(Vector2)gameObject.transform.position + goalPos;
        if (moveDirection != Vector3.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    public void BoD()
    {

    }

    Transform GetTargetTransform()
    {
        if (target == Target.Player)
        {
            return PlayerStats.instance.transform;
        }
        else
        {
            var transforms = GameObject.FindGameObjectsWithTag("Enemy");
            Transform nearestTransform = null;

            foreach (var t in transforms)
            {
                if (t == null) continue; // Skip null entries in the array
                Vector2 distanceSqr = t.transform.position - transform.position;
                if (distanceSqr.magnitude < nearestDistanceSqr)
                {
                    nearestTransform = t.transform;
                    nearestDistanceSqr = distanceSqr.magnitude;
                }
            }

            return nearestTransform;
        }
    }
}
