using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [Header("Level")]
    [SerializeField] List<GameObject> playLevelList;
    [SerializeField] GameObject tutorialLevel;
    bool tutorial = false;
    public int currentLevelIndex { get; private set; }
    GameObject currentLevel;
    public bool checkClear { get; private set; }
    Animator animator;

    public delegate void clearWhenLoadLevel();
    public clearWhenLoadLevel clearOnLoadLevel;

    void Start()
    {
        checkClear = false;
        if (GameManager.instance)
        {
            if (GameManager.instance.gameData == null)
            {
                return;
            }
            tutorial = GameManager.instance.gameData.tutorial;
            currentLevelIndex = GameManager.instance.gameData.currentLevelIndex;
        }
        else
        {
            currentLevelIndex = 0;
        }
        if (tutorial)
        {
            CreateTutorialLevel();
        }
        else
        {
            CreateLevel();
        }
        animator = GetComponent<Animator>();
    }
    void Awake()
    {
        instance = this;
    }

    void CreateLevel()
    {
        var go = Instantiate(playLevelList[currentLevelIndex], this.gameObject.transform) as GameObject;
        currentLevel = go;
    }

    void CreateTutorialLevel()
    {
        var tutorialLevelGo = Instantiate(tutorialLevel, this.gameObject.transform) as GameObject;
        currentLevel = tutorialLevelGo;
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
        GameManager.instance.SaveGame();
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
        var numMonster = currentLevel.GetComponentsInChildren<Monster_Behavior>().ToList();
        numMonster.RemoveAll(e => e.GetComponent<Entity>().isAlive == false || e.monsterType == Defines.MonsterType.Ally);
        if (numMonster.Count > 0)
        {
            checkClear = false;
            return;
        }
        var numAreaS = currentLevel.GetComponentsInChildren<SpawnEnemy>();
        foreach (var area in numAreaS)
        {
            area.monsterList.RemoveAll(e => e == null);
            if (area.monsterList.Any(e => e.GetComponent<Entity>().isAlive))
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
