using UnityEngine;
public class ProjectileNormal : ProjectileBehavior
{
    protected override void GetGoalPos()
    {
        originPos = transform.position;
        goalPos = new Vector2(originPos.x + 1 * this.direction, originPos.y);
        Vector2 direction = goalPos - originPos;
        direction = new Vector2(30f * direction.x, 30f * direction.y);
        goalPos = originPos + direction;
        gameObject.SetActive(true);
    }

    public override void Behaving()
    {
        if ((Vector2)transform.position == goalPos) Destroy(gameObject);
        transform.position = Vector2.MoveTowards(transform.position, goalPos, projectile.speed * Time.deltaTime);
        Vector3 moveDirection = -(Vector2)gameObject.transform.position + goalPos;
        if (moveDirection != Vector3.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}