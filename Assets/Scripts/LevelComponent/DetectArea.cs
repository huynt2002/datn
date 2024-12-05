using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectArea : MonoBehaviour
{
    public List<Monster_Behavior> monsters;
    public BoxCollider2D detectBound { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        monsters = new List<Monster_Behavior>();
        detectBound = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Monster_Behavior monster_Behavior = other.gameObject.GetComponentInParent<Monster_Behavior>();
            if (!monsters.Contains(monster_Behavior))
                monsters.Add(monster_Behavior);
            monster_Behavior.detectArea = this;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Monster_Behavior monster_Behavior = other.gameObject.GetComponentInParent<Monster_Behavior>();
            if (monsters.Contains(monster_Behavior))
            {
                //monster_Behavior.detectArea = null;
                monsters.Remove(monster_Behavior);
            }
        }
        else if (other.tag == "Player")
        {
            foreach (var mon in monsters)
            {
                mon.PlayerDetected(null);
            }
        }
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            foreach (var mon in monsters)
            {
                mon.PlayerDetected(other.GetComponentInParent<Entity>().transform);
            }
        }
    }

    // private void OnDrawGizmos()
    // {
    //     Gizmos.color = Color.red;
    //     Vector3 s = new Vector3(detectBound.size.x - 4f, detectBound.size.y, 0);
    //     Gizmos.DrawCube(gameObject.transform.position, s);
    // }
}
