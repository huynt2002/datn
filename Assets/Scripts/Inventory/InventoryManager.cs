using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance { get; private set; }
    public List<ItemStats> items { get; private set; }
    public const int numItem = 9;
    public bool full { get; private set; }
    PlayerStats p;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        items = new List<ItemStats>();
        p = PlayerStats.instance;
        CalculateItemsStats();
    }
    // Update is called once per frame
    void Update()
    {
        if (items.Count < numItem)
        {
            full = false;
        }
        else full = true;
    }
    public void AddItem(ItemManager item)
    {
        items.Add(item.itemStats);
        ApplyItemStats(item.itemStats);
    }

    public void RemoveItem(ItemManager item)
    {
        items.Remove(item.itemStats);
        CalculateItemsStats();
    }

    public void DropItem(ItemStats i)
    {
        SpawnManager.instance.SpawnItem(transform.position, i);
        RemoveItemStats(i);
        items.Remove(i);
        CalculateItemsStats();
    }

    void CalculateItemsStats()
    {
        p.SetDefault();
        float sumHP = 0;
        float sumDef = 0;
        float sumDMG = 0;
        foreach (var i in items)
        {
            sumHP += i.HPAmount;
            sumDef += i.DEFAmount;
            sumDMG += i.ATKAmount;
        }
        ItemStats tmp = new ItemStats();
        tmp.HPAmount = sumHP;
        tmp.DEFAmount = sumDef;
        tmp.ATKAmount = sumDMG;
        ApplyItemStats(tmp);
    }

    void ApplyItemStats(ItemStats s)
    {
        p.IncreaseHP(s.HPAmount);
        p.SetDEF(p.DEF * (1 + s.DEFAmount / 100));
        p.SetDMG(p.Damage * (1 + s.ATKAmount / 100));
    }

    void RemoveItemStats(ItemStats s)
    {
        p.IncreaseHP(-s.HPAmount);
    }
}
