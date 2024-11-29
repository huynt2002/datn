using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Level : MonoBehaviour
{
    GameObject mainCam;
    Collider2D mainBound;
    public List<Transform> chestSpawnPos;
    public List<GameObject> itemList;
    public List<GameObject> monsterList;
    // Start is called before the first frame update
    void Awake()
    {
        LevelManager.instance.clearOnLoadLevel += ClearLevel;
    }
    void Start()
    {
        mainCam = GameObject.FindWithTag("MainCamera");
        mainBound = GetComponent<PolygonCollider2D>();
        mainCam.GetComponent<CinemachineVirtualCamera>().m_Follow = PlayerStats.instance.transform;
        mainCam.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = mainBound;
        SpawnChest();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance) return;
        if (GameManager.instance.isBossFight)
        {

        }
        else
        {
            if (mainCam.GetComponent<CinemachineVirtualCamera>().m_Follow != PlayerStats.instance.transform)
                mainCam.GetComponent<CinemachineVirtualCamera>().m_Follow = PlayerStats.instance.transform;
        }
    }

    void SpawnChest()
    {
        var cPos = GameObject.FindGameObjectsWithTag("ChestPos");
        foreach (var i in cPos)
        {
            SpawnManager.instance.SpawnChest(i.transform);
        }
    }

    void ClearLevel()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        LevelManager.instance.clearOnLoadLevel -= ClearLevel;
    }
}
