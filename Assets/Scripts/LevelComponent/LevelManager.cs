using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [Header("Level")]
    [SerializeField] List<GameObject> playLevelList;
    int currentLevelIndex;
    GameObject currentLevel;
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
        instance = this;
        checkClear = false;
        currentLevelIndex = 0;
        CreateLevel();
    }


    void CreateLevel()
    {
        var go = Instantiate(playLevelList[currentLevelIndex], this.gameObject.transform) as GameObject;
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

    public void InitLV()
    {
        if (currentLevelIndex == playLevelList.Count)
        {
            GameManager.instance.isWin = true;
            return;
        }
        checkClear = false;
        ResetWhenTransis();
        CreateLevel();
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
        var numMonster = currentLevel.GetComponentsInChildren<Monster_Behavior>();
        if (numMonster.Length > 0)
        {
            checkClear = false;
            return;
        }
        var numAreaS = currentLevel.GetComponentsInChildren<SpawnEnemy>();
        foreach (var area in numAreaS)
        {
            if (area.monsterList.ToArray().Any(e => e != null))
            {
                checkClear = false; return;
            }
        }
        checkClear = true;
        currentLevelIndex++;
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
