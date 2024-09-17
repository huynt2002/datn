using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [Header("Enemy")]
    public int[] numEnemy;
    public GameObject[] enemy;
    [Header("SpawnType")]
    public SpawnType spawnType = SpawnType.Wait;
    public enum SpawnType
    {
        Trigger,
        Wait
    }
    public float SpawnTime = 1f;
    public List<GameManager> monster;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (spawnType == SpawnType.Wait)
        {
            if (SpawnTime > 0)
            {
                SpawnTime -= Time.deltaTime;
            }
            else
            {
                Spawn();
            }
        }
    }

    void Spawn()
    {
        for (int i = 0; i < enemy.LongLength; i++)
            while (numEnemy[i] > 0)
            {
                Vector2 pos = new Vector2(Random.Range(transform.position.x - gameObject.GetComponent<BoxCollider2D>().bounds.size.x / 2 + 1f,
                 transform.position.x + gameObject.GetComponent<BoxCollider2D>().bounds.size.x / 2 - 1f),
                 transform.position.y + 0.5f);
                var mons = SpawnManager.instance.SpawnMonster(enemy[i], pos) as GameObject;
                mons.transform.parent = transform;
                mons.SetActive(false);
                numEnemy[i]--;
            }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && spawnType == SpawnType.Trigger)
        {
            Spawn();
        }
    }
}
