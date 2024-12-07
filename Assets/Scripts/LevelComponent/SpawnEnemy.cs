using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [Header("Enemy")]
    public int[] numEnemy;
    [SerializeField] GameObject[] enemyPrefabList;
    [Header("SpawnType")]
    public SpawnType spawnType = SpawnType.Wait;
    public enum SpawnType
    {
        Trigger,
        Wait
    }
    public float spawnTime = 1f;
    public List<GameObject> monsterList { get; private set; } = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < enemyPrefabList.Length; i++)
            while (numEnemy[i] > 0)
            {
                Vector2 pos = new Vector2(Random.Range(transform.position.x - gameObject.GetComponent<BoxCollider2D>().bounds.size.x / 2 + 1f,
                 transform.position.x + gameObject.GetComponent<BoxCollider2D>().bounds.size.x / 2 - 1f),
                 transform.position.y + 0.5f);
                var mons = SpawnManager.instance.SpawnMonster(enemyPrefabList[i], pos, transform, Defines.MonsterType.Enemy);
                mons.SetActive(false);
                monsterList.Add(mons);
                numEnemy[i]--;
            }
    }

    // Update is called once per frame
    void Update()
    {
        if (monsterList.Count > 0)
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
        monsterList.Clear();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == Defines.Tag.Player && spawnType == SpawnType.Trigger)
        {
            Spawn();
        }
    }
}
