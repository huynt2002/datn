using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : PlayerInteract
{
    public ItemStats itemStats;
    public string Name;
    public float ATKAmount;
    public float DEFAmount;
    public float HPAmount;
    public string description;
    Sprite icon;

    public bool isSale { get; private set; }
    public int cost = 500;
    void Awake()
    {
        if (LevelManager.instance)
            LevelManager.instance.clearOnLoadLevel += DestroyItem;
    }

    void Start()
    {
        interactType = InteractType.Item;
        ApplyData();
    }

    void OnDestroy()
    {
        if (LevelManager.instance)
            LevelManager.instance.clearOnLoadLevel -= DestroyItem;
    }

    void Update()
    {

    }
    public void ApplyData()
    {
        Name = itemStats.ItemName;
        ATKAmount = itemStats.ATKAmount;
        DEFAmount = itemStats.DEFAmount;
        HPAmount = itemStats.HPAmount;
        description = itemStats.description;
        icon = itemStats.icon;
        GetComponent<SpriteRenderer>().sprite = icon;
    }
    public override void Item()
    {
        if (isSale)
        {
            var coin = PlayerStats.instance.coin;
            if (coin >= cost)
            {
                PlayerStats.instance.SetCoin(coin - cost);
                isSale = false;
                GetPickedUp(InventoryManager.instance);
            }
            else
            {
                return;
            }
        }
        else
        {
            GetPickedUp(InventoryManager.instance);
        }
    }

    void GetPickedUp(InventoryManager inventoryManager)
    {
        if (inventoryManager.full)
        {
            return;
        }
        inventoryManager.AddItem(this);
        Destroy(this.gameObject);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!isSale)
        {
            InfoUIManager.instance.SetInfo(gameObject.transform, Color.white, Defines.InfoButText.PickUp,
                    itemStats.ItemName, itemStats.itemType.ToString(), itemStats.description);
        }
        else
        {
            InfoUIManager.instance.SetInfo(gameObject.transform, Color.yellow, Defines.InfoButText.Buy + " (" + cost + ")",
                      itemStats.ItemName, itemStats.itemType.ToString(), itemStats.description);
        }
    }

    public void SetSale()
    {
        isSale = true;
    }

    void DestroyItem()
    {
        Destroy(gameObject);
    }
}
