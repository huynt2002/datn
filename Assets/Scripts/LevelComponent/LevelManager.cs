using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [Header("Level")]
    public GameObject homeLevel;
    public List<GameObject> playLevel;
    List<GameObject> playLevelList;
    public int numLevel;
    int numLevelPass = 0;
    GameObject currentLevel;
    public GameObject lastLevel;
    public GameObject marketLevel;
    public bool checkClear { get; private set; }
    Animator animator;

    public delegate void clearWhenLoadLevel();
    public clearWhenLoadLevel clearOnLoadLevel;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Awake()
    {
        numLevelPass = 0;
        instance = this;
        CreatePlayLevelList();
        checkClear = false;
        HomeLevel();
    }

    void CreatePlayLevelList()
    {
        playLevelList = new List<GameObject>();
        foreach (var x in playLevel)
        {
            playLevelList.Add(x);
        }
        if (playLevelList.Count > numLevel)
        {
            int x = playLevelList.Count - numLevel;
            for (int i = 0; i < x; i++)
            {
                int r = Random.Range(0, playLevelList.Count - 1);
                playLevelList.RemoveAt(r);
            }
        }
    }

    void HomeLevel()
    {
        var go = Instantiate(homeLevel, this.gameObject.transform) as GameObject;
        currentLevel = go;
        go.transform.parent = this.gameObject.transform;
    }

    void Update()
    {
        if (!checkClear)
        {
            CheckLevelClear();
        }
    }
    void InitLastLV()
    {
        playLevelList.Clear();
        var go = Instantiate(lastLevel, this.gameObject.transform) as GameObject;
        currentLevel = go;
        go.transform.parent = this.gameObject.transform;
    }

    void InitMarket()
    {
        var go = Instantiate(marketLevel, this.gameObject.transform) as GameObject;
        currentLevel = go;
        go.transform.parent = this.gameObject.transform;
        numLevelPass = 0;
    }

    void InitNormalLevel()
    {
        int ran = Random.Range(0, playLevelList.Count);
        var go = Instantiate(playLevelList[ran], this.gameObject.transform) as GameObject;
        playLevelList.Remove(playLevelList[ran]);
        currentLevel = go;
        go.transform.parent = this.gameObject.transform;
        numLevelPass++;
    }
    public void InitLV()
    {
        if (currentLevel.name == lastLevel.name + "(Clone)")
        {
            GameManager.instance.isWin = true;
            return;
        }
        checkClear = false;
        ResetWhenTransis();
        if (numLevelPass % 2 == 0 && numLevelPass > 0)
        {
            InitMarket();
        }
        else if (playLevelList.Count == 0)
        {
            InitLastLV();
        }
        else
        {
            InitNormalLevel();
        }
    }



    public void ResetWhenTransis()
    {
        if (currentLevel)
        {
            currentLevel.SetActive(false);
            Destroy(currentLevel);
            var p = PlayerStats.instance.transform;
            p.position = Vector3.zero;
            p.localScale = new Vector3(1, 1, 1);
        }
        clearOnLoadLevel?.Invoke();
    }

    void CheckLevelClear()
    {
        if (!currentLevel)
        {
            return;
        }
        // var numArea = currentLevel.GetComponentsInChildren<DetectArea>();
        // foreach (var area in numArea)
        // {
        //     if (area.monsters.Count > 0) { checkClear = false; return; }
        // }
        var numAreaS = currentLevel.GetComponentsInChildren<SpawnEnemy>();
        foreach (var area in numAreaS)
        {
            for (int i = 0; i < area.numEnemy.Length; i++)
            {
                if (area.numEnemy[i] > 0) { checkClear = false; return; }
            }
        }
        var ms = GameObject.FindGameObjectsWithTag("Enemy");
        if (ms.Length > 0)
        {
            checkClear = false;
            return;
        }
        checkClear = true;
    }

    public void Transis(bool load = true)
    {
        if (checkClear)
        {
            if (load)
            {
                animator.SetTrigger("load");
            }
            else
            {
                animator.SetTrigger("transis");
            }
        }
    }
}
