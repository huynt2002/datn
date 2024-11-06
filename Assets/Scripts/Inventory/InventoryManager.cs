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

    // public Dictionary<ItemTrait, int> itemTraits;

    // public Dictionary<ItemTrait, GameObject> effectList;

    void Start()
    {
        instance = this;
        items = new List<KeyValuePair<ItemStats, GameObject>>();
        p = gameObject.GetComponent<Entity>();
        // CalculateItemsStats(p);
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

    // void CalculateItemsStats(Entity e)
    // {
    //     e.SetDefault();
    //     foreach (var i in items)
    //     {
    //         i.Key.ApplyItemStats(e);
    //     }
    // }

    public void HandleItemEffect(ItemStats item)
    {


    }

    public void RemoveItemEffect()
    {

    }

    public void DropItem(int index)
    {
        var item = items[index];
        SpawnManager.instance.SpawnItem(transform.position, item.Key);
        item.Key.RemoveItemStats(p);
        items.Remove(item);
        //CalculateItemsStats(p);
    }
}
