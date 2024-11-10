using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance { get; private set; }
    public List<KeyValuePair<ItemStats, GameObject>> items { get; private set; }
    public const int numItem = 9;
    public bool full { get; private set; }
    Entity p;

    [SerializeField] GameObject itemEffectContainer;

    public Dictionary<ItemTrait, int> itemTraitCount;

    public Dictionary<ItemTrait, GameObject> traitEffectList;

    void Start()
    {
        instance = this;
        items = new List<KeyValuePair<ItemStats, GameObject>>();
        p = gameObject.GetComponent<Entity>();
        // CalculateItemsStats(p);
        itemTraitCount = new Dictionary<ItemTrait, int>();
        traitEffectList = new Dictionary<ItemTrait, GameObject>();
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
    public void AddItem(ItemStats item)
    {
        if (item.itemEffectObject)
        {
            var itemEffect = Instantiate(item.itemEffectObject, itemEffectContainer.transform) as GameObject;
            items.Add(new KeyValuePair<ItemStats, GameObject>(item, itemEffect));
        }
        else
        {
            items.Add(new KeyValuePair<ItemStats, GameObject>(item, null));
        }
        item.ApplyItemStats(p);
        AddItemTrait(item.trait);
    }

    void AddItemTrait(ItemTrait trait)
    {
        if (!trait) return;
        if (itemTraitCount.ContainsKey(trait))
        {
            itemTraitCount[trait]++;
        }
        else
        {
            itemTraitCount[trait] = 1;
        }
        UpdateItemTraitEffect(trait);
        Debug.Log("A: " + itemTraitCount);
    }

    void RemoveItemTrait(ItemTrait trait)
    {
        if (!trait) return;
        if (itemTraitCount.ContainsKey(trait))
        {
            itemTraitCount[trait]--;
        }
        UpdateItemTraitEffect(trait);
        Debug.Log("A: " + itemTraitCount);
    }

    // void CalculateItemsStats(Entity e)
    // {
    //     e.SetDefault();
    //     foreach (var i in items)
    //     {
    //         i.Key.ApplyItemStats(e);
    //     }
    // }

    public void UpdateItemTraitEffect(ItemTrait itemTrait)
    {
        itemTrait.RemoveStats(p);
        if (traitEffectList.ContainsKey(itemTrait))
        {
            Destroy(traitEffectList[itemTrait]);
        }
        itemTrait.UpdateCurrentLevel(itemTraitCount[itemTrait]);
        itemTrait.ApplyStats(p);
        traitEffectList[itemTrait] = itemTrait.GetTraitEffect(itemEffectContainer.transform);
    }

    public void DropItem(int index)
    {
        var item = items[index];
        SpawnManager.instance.SpawnItem(transform.position, item.Key);
        item.Key.RemoveItemStats(p);
        items.Remove(item);
        Destroy(item.Value);
        RemoveItemTrait(item.Key.trait);
        //CalculateItemsStats(p);
    }
}
