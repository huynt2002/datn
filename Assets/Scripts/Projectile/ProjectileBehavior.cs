using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public enum ProjectileType
    {
        Chase, BoD, Fireball
    }

    [SerializeField] ProjectileType type;
    public Vector2 originPos;
    public Vector2 goalPos;
    public float speed;
    private float damage;
    bool damaged;
    // Start is called before the first frame update
    [SerializeField]
    public void Set(float damage = 10, float speed = 7f)
    {
        this.damage = damage;
        this.speed = speed;
    }

    void Start()
    {
        damaged = false;
        Vector2 pPos = PlayerStats.instance.transform.position;
        if (type == ProjectileType.Chase)
        {
            originPos = transform.position;
            goalPos = pPos;
            Vector2 direction = goalPos - originPos;
            direction = new Vector2(10f * direction.x, 10f * direction.y);
            goalPos = originPos + direction;
        }
        else if (type == ProjectileType.BoD)
        {

        }
        else if (type == ProjectileType.Fireball)
        {
            originPos = transform.position;
            goalPos = new Vector2(originPos.x + 1 * PlayerStats.instance.transform.localScale.x, originPos.y);
            Vector2 direction = goalPos - originPos;
            direction = new Vector2(30f * direction.x, 30f * direction.y);
            goalPos = originPos + direction;
        }
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
        if (type == ProjectileType.Chase)
        {
            if (damaged) return;
            other.transform.parent.GetComponent<Entity>().TakeDamage(damage, Defines.DamageType.Projectile);
            damaged = true;
            // if (type == ProjectileType.Chase)
            //     Destroy(gameObject);
        }
        else
        {
            other.transform.parent.GetComponent<Entity>().TakeDamage(damage, Defines.DamageType.Projectile);
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
}
