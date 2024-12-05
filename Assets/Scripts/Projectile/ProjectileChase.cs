using UnityEngine;
public class ProjectileChase : ProjectileBehavior
{
    public enum Target
    {
        Player, Enemy
    }
    Transform targetPos;
    [SerializeField] Target target;
    public float nearestDistanceSqr = 30f;

    protected override void GetGoalPos()
    {
        targetPos = GetTargetTransform();
        if (targetPos)
        {
            UpdateTargetPos(targetPos.position);
        }
        else
        {
            Destroy(gameObject);
        }
        gameObject.SetActive(true);
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

    void UpdateTargetPos(Vector2 targetPos)
    {
        originPos = transform.position;
        Vector2 direction = targetPos - originPos;
        direction = new Vector2(10f * direction.x, 10f * direction.y);
        goalPos = originPos + direction;
    }
}