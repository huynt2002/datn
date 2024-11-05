using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance { get; private set; }
    public Dictionary<ItemStats, GameObject> items { get; private set; }
    public const int numItem = 9;
    public bool full { get; private set; }
    Entity p;

    public GameObject itemEffectContainer;

    // public Dictionary<ItemTrait, int> itemTraits;

    // public Dictionary<ItemTrait, GameObject> effectList;

    void Start()
    {
        instance = this;
        items = new Dictionary<ItemStats, GameObject>();
        p = gameObject.GetComponent<Entity>();
        CalculateItemsStats(p);
        // itemTraits = new Dictionary<ItemTrait, int>();
        // effectList = new Dictionary<ItemTrait, GameObject>();
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
        //AddItemTrait(item.itemStats.trait);
        var itemEffect = Instantiate(item.itemEffectObject, itemEffectContainer.transform) as GameObject;
        items.Add(item, itemEffect);
        item.ApplyItemStats(p);
    }

    // void AddItemTrait(ItemTrait trait)
    // {
    //     if (itemTraits.ContainsKey(trait))
    //     {
    //         itemTraits[trait]++;
    //     }
    //     else
    //     {
    //         itemTraits[trait] = 1;
    //     }
    //     HandleItemEffect(trait);
    // }

    // void RemoveItemTrait(ItemTrait trait)
    // {
    //     if (itemTraits.ContainsKey(trait))
    //     {
    //         itemTraits[trait]--;
    //     }
    //     else
    //     {
    //         itemTraits[trait] = 0;
    //     }
    //     HandleItemEffect(trait);
    // }

    void CalculateItemsStats(Entity e)
    {
        e.SetDefault();
        foreach (var i in items.Keys)
        {
            i.ApplyItemStats(e);
        }
    }

    public void RemoveItem(ItemStats item)
    {
        //RemoveItemTrait(item.itemStats.trait);
        Destroy(items[item]);
        items.Remove(item);
        CalculateItemsStats(p);
    }

    public void HandleItemEffect(ItemStats item)
    {


    }

    public void RemoveItemEffect()
    {

    }

    public void DropItem(int index)
    {
        var item = items.Keys.ToList()[index];
        SpawnManager.instance.SpawnItem(transform.position, item);
        item.RemoveItemStats(p);
        items.Remove(item);
        CalculateItemsStats(p);
    }
}
