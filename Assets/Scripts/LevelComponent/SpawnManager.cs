
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
    [SerializeField] List<ItemStats> itemDataList;
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
        None, PlayerJumpEffect, PlayerDashEffect, HitEffect
    }

    public enum ParticleType
    {
        BloodSmall, BloodLarge
    }

    public GameObject SpawnEffect(EffectType effectType, Vector2 pos)
    {
        GameObject effect = Instantiate(effectController, pos, Quaternion.identity) as GameObject;
        switch (effectType)
        {
            case EffectType.PlayerJumpEffect:
                effect.GetComponent<ParticleDestroyEvent>().effectType = EffectType.PlayerJumpEffect;
                return effect;
            case EffectType.PlayerDashEffect:
                effect.GetComponent<ParticleDestroyEvent>().effectType = EffectType.PlayerDashEffect;
                return effect;
            case EffectType.HitEffect:
                effect.GetComponent<ParticleDestroyEvent>().effectType = EffectType.HitEffect;
                return effect;
        }
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
        foreach (var item in itemDataList)
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

    // Update is called once per frame
    public GameObject SpawnMonster(GameObject pre, Vector2 pos)
    {
        //SpawnEnemy
        GameObject e = Instantiate(pre, pos, Quaternion.identity) as GameObject;
        //AttachUIElement
        GameObject ui = Instantiate(uiHPCanvas, pos, Quaternion.identity) as GameObject;
        ui.GetComponent<DisplayHP>().Set(e.GetComponent<Entity>(), e.transform);
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
                i = Random.Range(0, itemDataList.Count);
                itemStats = itemDataList[i];
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

    public void DropProps(Transform pos, DropedProp.DropType type)
    {
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
