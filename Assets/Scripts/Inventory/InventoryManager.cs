using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance { get; private set; }
    public List<KeyValuePair<ItemStats, GameObject>> items { get; private set; } = new List<KeyValuePair<ItemStats, GameObject>>();
    public const int numItem = 9;
    public bool full { get; private set; }
    Entity p;

    [SerializeField] GameObject itemEffectContainer;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        p = gameObject.GetComponent<PlayerStats>();
        LoadInventoryData();
    }

    void LoadInventoryData()
    {
        if (GameManager.instance.gameData == null)
        {
            return;
        }
        if (GameManager.instance.gameData.inventoryItemIds != null)
        {
            var ids = GameManager.instance.gameData.inventoryItemIds;
            foreach (var itemId in ids)
            {
                var item = SpawnManager.instance.itemDataList.items.Find(e => e.id == itemId);
                if (item)
                {
                    AddItem(item, false);
                }
            }
        }
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
    public void AddItem(ItemStats item, bool applyStats = true)
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
        if (applyStats) { item.ApplyItemStats(p); }
    }

    public void DropItem(int index)
    {
        var item = items[index];
        SpawnManager.instance.SpawnItem(transform.position, item.Key);
        item.Key.RemoveItemStats(p);
        items.Remove(item);
        Destroy(item.Value);
    }
}
