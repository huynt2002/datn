
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("UIElement")]
    [SerializeField] GameObject uiHPCanvas;
    public static SpawnManager instance;
    [Header("Monster")]
    //[SerializeField] Dictionary<string> itemPre;

    [Header("Item")]
    [SerializeField] GameObject itemPre;
    [SerializeField] public ItemPool itemDataList;
    List<ItemStats> itemCommonDataList;
    List<ItemStats> itemRareDataList;
    List<ItemStats> itemLegendDataList;

    [Header("Chest")]

    [SerializeField] GameObject wooden;
    [SerializeField] GameObject silver;
    [SerializeField] GameObject gold;

    [Header("DropProps")]
    [SerializeField] GameObject HPDrop;
    public int hpDropRate;
    [SerializeField] GameObject coin;
    public int coinDropRate;
    [SerializeField] GameObject gem;
    public int gemDropRate;

    [Header("2D Effect")]
    [SerializeField] GameObject effectController;

    [Header("Particle Effect")]
    [SerializeField] GameObject bloodSmall;
    [SerializeField] GameObject bloodLarge;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Awake()
    {
        instance = this;
        InitItemList();
    }

    public enum EffectType
    {
        None, PlayerJumpEffect, PlayerDashEffect, HitEffect, PlayerHealingEffect
    }

    public enum ParticleType
    {
        BloodSmall, BloodLarge
    }

    public GameObject SpawnEffect(EffectType effectType, Vector2 pos)
    {
        GameObject effect = Instantiate(effectController, pos, Quaternion.identity) as GameObject;
        effect.GetComponent<ParticleDestroyEvent>().effectType = effectType;
        return effect;
    }

    public GameObject SpawnEffect(EffectType effectType, Transform parent, Vector2 pos)
    {
        GameObject effect = Instantiate(effectController, pos, Quaternion.identity) as GameObject;
        effect.transform.parent = parent;
        effect.GetComponent<ParticleDestroyEvent>().effectType = effectType;
        return effect;
    }

    public GameObject SpawnParticalEffect(ParticleType type, Vector2 pos)
    {
        switch (type)
        {
            case ParticleType.BloodSmall:
                return Instantiate(bloodSmall, pos, Quaternion.identity) as GameObject;
            case ParticleType.BloodLarge:
                return Instantiate(bloodLarge, pos, Quaternion.identity) as GameObject;
        }
        return null;
    }

    void InitItemList()
    {
        itemCommonDataList = new List<ItemStats>();
        itemRareDataList = new List<ItemStats>();
        itemLegendDataList = new List<ItemStats>();
        foreach (var item in itemDataList.items)
        {
            switch (item.itemType)
            {
                case ItemStats.ItemType.Common:
                    itemCommonDataList.Add(item);
                    break;
                case ItemStats.ItemType.Rare:
                    itemRareDataList.Add(item);
                    break;
                case ItemStats.ItemType.Legend:
                    itemLegendDataList.Add(item);
                    break;
            }
        }
    }

    public enum ChestType
    {
        None, Wooden, Silver, Gold
    }

    public GameObject SpawnMonster(GameObject pre, Vector2 pos, Transform parent, Defines.MonsterType type)
    {
        //SpawnEnemy
        GameObject e = Instantiate(pre, pos, Quaternion.identity, parent);
        e.GetComponent<Monster_Behavior>().monsterType = type;
        //AttachUIElement
        GameObject ui = Instantiate(uiHPCanvas, pos, Quaternion.identity, e.transform);
        ui.GetComponent<DisplayHP>().Set(e.GetComponent<Entity>(), type);
        return e;
    }

    public GameObject SpawnItem(Vector2 pos, ItemStats itemStats = null)
    {
        if (itemStats == null)
        {
            itemStats = ChoseItem();
        }
        var item = Instantiate(itemPre, pos, Quaternion.identity) as GameObject;
        item.GetComponent<ItemManager>().itemStats = itemStats;
        return item;
    }

    public GameObject SpawnItemFromChest(Vector2 pos, ChestType chestType = ChestType.None)
    {
        ItemStats itemStats = ChoseItem(chestType);
        var item = Instantiate(itemPre, pos, Quaternion.identity) as GameObject;
        item.GetComponent<ItemManager>().itemStats = itemStats;
        return item;
    }

    ItemStats ChoseItem(ChestType type = ChestType.None)
    {
        ItemStats itemStats;
        int i = -1;
        switch (type)
        {
            case ChestType.Wooden:
                i = Random.Range(0, itemCommonDataList.Count);
                itemStats = itemCommonDataList[i];
                break;
            case ChestType.Silver:
                i = Random.Range(0, itemRareDataList.Count);
                itemStats = itemRareDataList[i];
                break;
            case ChestType.Gold:
                i = Random.Range(0, itemLegendDataList.Count);
                itemStats = itemLegendDataList[i];
                break;
            default:
                i = Random.Range(0, itemDataList.items.Count);
                itemStats = itemDataList.items[i];
                break;
        }
        return itemStats;
    }

    public void SpawnChest(Transform pos, ChestType chestType = ChestType.None)
    {
        GameObject prefa = ChestTypeToGameObject(chestType);
        var chest = Instantiate(prefa, pos.position, Quaternion.identity) as GameObject;
        chest.transform.parent = pos;
    }

    GameObject ChestTypeToGameObject(ChestType chestType)
    {
        GameObject prefa;
        if (chestType == ChestType.Wooden)
        {
            prefa = wooden;
        }
        else if (chestType == ChestType.Silver)
        {
            prefa = silver;
        }
        else if (chestType == ChestType.Gold)
        {
            prefa = gold;
        }
        else
        {
            int i = Random.Range(0, 101);
            if (i < 50)
            {
                prefa = wooden;
            }
            else if (i < 85)
            {
                prefa = silver;
            }
            else
            {
                prefa = gold;
            }
        }
        return prefa;
    }

    void DropProps(Transform pos, DropedProp.DropType type)
    {
        pos.position = new Vector2(pos.position.x, pos.position.y + 0.5f);
        if (type == DropedProp.DropType.HP)
        {
            var go = Instantiate(HPDrop, pos.position, Quaternion.identity) as GameObject;
        }
        else if (type == DropedProp.DropType.Coin)
        {
            var go = Instantiate(coin, pos.position, Quaternion.identity) as GameObject;
        }
        else if (type == DropedProp.DropType.Gem)
        {
            var go = Instantiate(gem, pos.position, Quaternion.identity) as GameObject;
        }
    }

    public void DropProps(Transform pos)
    {
        var HPrate = Random.Range(0, 101);
        var coinRate = Random.Range(0, 101);
        var gemRate = Random.Range(0, 101);

        if (HPrate < hpDropRate)
        {
            DropProps(pos, DropedProp.DropType.HP);
        }
        else { }

        if (coinRate < coinDropRate)
        {
            DropProps(pos, DropedProp.DropType.Coin);
        }
        else { }

        if (gemRate < gemDropRate)
        {
            DropProps(pos, DropedProp.DropType.Gem);
        }
        else { }
    }

}
