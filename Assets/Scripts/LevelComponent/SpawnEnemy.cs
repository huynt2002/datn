using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [Header("Enemy")]
    public int[] numEnemy;
    public GameObject[] enemyPrefabList;
    [Header("SpawnType")]
    private bool spawned = false;
    public SpawnType spawnType = SpawnType.Wait;
    public enum SpawnType
    {
        Trigger,
        Wait
    }
    public float spawnTime = 1f;
    ArrayList monsterList = new ArrayList();
    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < enemyPrefabList.Length; i++)
            while (numEnemy[i] > 0)
            {
                Vector2 pos = new Vector2(Random.Range(transform.position.x - gameObject.GetComponent<BoxCollider2D>().bounds.size.x / 2 + 1f,
                 transform.position.x + gameObject.GetComponent<BoxCollider2D>().bounds.size.x / 2 - 1f),
                 transform.position.y + 0.5f);
                var mons = SpawnManager.instance.SpawnMonster(enemyPrefabList[i], pos) as GameObject;
                mons.transform.parent = gameObject.transform;
                mons.SetActive(false);
                monsterList.Add(mons);
                numEnemy[i]--;
            }
    }

    // Update is called once per frame
    void Update()
    {
        if (!spawned)
            if (spawnType == SpawnType.Wait)
            {
                if (spawnTime > 0)
                {
                    spawnTime -= Time.deltaTime;
                }
                else
                {
                    Spawn();
                }
            }
    }

    void Spawn()
    {
        foreach (GameObject monster in monsterList)
        {
            monster.SetActive(true);
        }
        spawned = true;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && spawnType == SpawnType.Trigger)
        {
            Spawn();
        }
    }
}
