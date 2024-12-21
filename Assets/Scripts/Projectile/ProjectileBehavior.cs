using UnityEngine;

public abstract class ProjectileBehavior : MonoBehaviour
{
    protected Vector2 originPos;
    protected Vector2 goalPos;
    public bool destroyedAtCollided = false;
    [SerializeField] protected Projectile projectile;
    protected int direction => projectile.direction;

    protected abstract void GetGoalPos();
    public abstract void Behaving();
    private void Start()
    {
        if (!projectile)
        {
            Debug.LogError("projectileObj null!");
        }
        GetGoalPos();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (destroyedAtCollided)
        {
            Destroy(gameObject);
        }
    }
}
