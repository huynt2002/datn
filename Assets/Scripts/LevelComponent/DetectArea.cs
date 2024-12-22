using System.Collections.Generic;
using UnityEngine;

public class DetectArea : MonoBehaviour
{
    public List<Monster_Behavior> monsters;
    public List<Monster_Behavior> allies;
    public Entity player;
    public BoxCollider2D detectBound { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        monsters = new List<Monster_Behavior>();
        detectBound = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == Defines.Tag.Enemy)
        {
            Monster_Behavior monster_Behavior = other.gameObject.GetComponentInParent<Monster_Behavior>();
            if (!monsters.Contains(monster_Behavior))
            {
                monster_Behavior.detectArea = this;
            }
            monsters.Add(monster_Behavior);
        }
        else if (other.tag == Defines.Tag.Ally)
        {
            Monster_Behavior monster_Behavior = other.gameObject.GetComponentInParent<Monster_Behavior>();
            if (!allies.Contains(monster_Behavior))
            {
                allies.Add(monster_Behavior);
            }
            monster_Behavior.detectArea = this;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == Defines.Tag.Enemy)
        {
            Monster_Behavior monster_Behavior = other.GetComponentInParent<Monster_Behavior>();
            if (monsters.Contains(monster_Behavior))
            {
                monster_Behavior.detectArea = null;
                monsters.Remove(monster_Behavior);
            }
        }
        else if (other.tag == Defines.Tag.Ally)
        {
            Monster_Behavior monster_Behavior = other.GetComponentInParent<Monster_Behavior>();
            if (allies.Contains(monster_Behavior))
            {
                monster_Behavior.detectArea = null;
                allies.Remove(monster_Behavior);
            }
        }
        else if (other.tag == Defines.Tag.Player)
        {
            foreach (var mon in monsters)
            {
                mon.SetAttackTarget(null);
                if (other.transform.position.x > detectBound.transform.position.x - detectBound.bounds.size.x / 2 + 1f &&
                other.transform.position.x < detectBound.transform.position.x + detectBound.bounds.size.x / 2 - 1f)
                {
                    mon.moveTarget = other.transform.position;
                }

            }
        }
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == Defines.Tag.Player)
        {
            foreach (var mon in monsters)
            {
                mon.SetAttackTarget(other.GetComponentInParent<Entity>().transform);
            }
        }
        else if (other.tag == Defines.Tag.Ally)
        {
            Monster_Behavior ally = other.GetComponentInParent<Monster_Behavior>();
            monsters.RemoveAll(e => e == null);
            if (ally.attackTarget == null && monsters.Count > 0)
            {
                var target = monsters[Random.Range(0, monsters.Count - 1)];
                if (target.attackTarget == null)
                {
                    target.SetAttackTarget(ally.transform);
                }
                ally.SetAttackTarget(target.transform);
            }
        }
    }
}
